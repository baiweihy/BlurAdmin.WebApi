using System;
using LegacyApplication.Shared.Features.Base;
using LegacyApplication.Shared.Features.File;

namespace LegacyApplication.ViewModels.Inventory
{
    public class InventoryItemViewModel : EntityBase, IFileEntity
    {
        public string ProductSerialNo => Warehouse?.Territory?.Name + Warehouse?.No + "-" + Type + "-" + Serial;
        public string SerialNo => Warehouse?.Territory?.No + Warehouse?.No + "-" + Type + "-" + Serial;
        public string SerialName => Warehouse?.Territory?.Name + Warehouse?.Name + "-" + Type + "-" + Serial;

        public string Type { get; set; }
        
        public string Serial { get; set; }

        public string Model { get; set; }

        public string Specification { get; set; }

        public string Manufacturer { get; set; }

        public string Phone { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime UseDate { get; set; }
        public string TechnicalStatus { get; set; }
        public string Remark { get; set; }

        public string SourceWay { get; set; }
        public string OriginalValues { get; set; }
        public string Location { get; set; }
        public string ChangingStatus { get; set; }
        public DateTime ChangeDate { get; set; }

        public int WarehouseId { get; set; }
        
        public WarehouseViewModel Warehouse { get; set; }

        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }
}
