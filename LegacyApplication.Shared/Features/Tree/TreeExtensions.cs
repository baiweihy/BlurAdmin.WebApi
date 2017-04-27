using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyApplication.Shared.Features.Tree
{
    public static class TreeExtensions
    {
        /// <summary>
        /// 把树形结构数据的集合转化成单一根结点的树形结构数据
        /// </summary>
        /// <typeparam name="T">树形结构实体</typeparam>
        /// <param name="items">树形结构实体的集合</param>
        /// <returns>树形结构实体的根结点</returns>
        public static TreeEntityBase<T> ToSingleRoot<T>(this IEnumerable<TreeEntityBase<T>> items) where T : TreeEntityBase<T>
        {
            var all = items.ToList();
            if (!all.Any())
            {
                return null;
            }
            var top = all.Where(x => x.ParentId == null).ToList();
            if (top.Count > 1)
            {
                throw new Exception("树的根节点数大于1个");
            }
            if (top.Count == 0)
            {
                throw new Exception("未能找到树的根节点");
            }
            TreeEntityBase<T> root = top.Single();

            Action<TreeEntityBase<T>> findChildren = null;
            findChildren = current =>
            {
                var children = all.Where(x => x.ParentId == current.Id).ToList();
                foreach (var child in children)
                {
                    findChildren(child);
                }
                current.Children = children as ICollection<T>;
            };

            findChildren(root);

            return root;
        }

        /// <summary>
        /// 把树形结构数据的集合转化成多个根结点的树形结构数据
        /// </summary>
        /// <typeparam name="T">树形结构实体</typeparam>
        /// <param name="items">树形结构实体的集合</param>
        /// <returns>多个树形结构实体根结点的集合</returns>
        public static List<TreeEntityBase<T>> ToMultipleRoots<T>(this IEnumerable<TreeEntityBase<T>> items) where T : TreeEntityBase<T>
        {
            List<TreeEntityBase<T>> roots;
            var all = items.ToList();
            if (!all.Any())
            {
                return null;
            }
            var top = all.Where(x => x.ParentId == null).ToList();
            if (top.Any())
            {
                roots = top;
            }
            else
            {
                throw new Exception("未能找到树的根节点");
            }

            Action<TreeEntityBase<T>> findChildren = null;
            findChildren = current =>
            {
                var children = all.Where(x => x.ParentId == current.Id).ToList();
                foreach (var child in children)
                {
                    findChildren(child);
                }
                current.Children = children as ICollection<T>;
            };

            roots.ForEach(findChildren);

            return roots;
        }

        /// <summary>
        /// 作为父节点, 取得树形结构实体的祖先ID串
        /// </summary>
        /// <typeparam name="T">树形结构实体</typeparam>
        /// <param name="parent">父节点实体</param>
        /// <returns></returns>
        public static string GetAncestorIdsAsParent<T>(this T parent) where T : TreeEntityBase<T>
        {
            return string.IsNullOrEmpty(parent.AncestorIds) ? parent.Id.ToString() : (parent.AncestorIds + "-" + parent.Id);
        }
    }
}
