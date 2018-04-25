using System;
using System.Collections.Generic;
namespace DFWinForms.LogicTree
{
    public interface IGroupLayer : IBaseLayer, IPopMenu
    {
        // Methods
        void Add(IBaseLayer layer);
        void Add(IBaseLayer layer, int index);
        void Add2(IBaseLayer layer);
        void Clear();
        void CollapseAll();
        void Delete(IBaseLayer layer);
        bool ExistLayer(string guid);
        bool ExistLayerName(string layername);
        void ExpandAll();
        bool ImportXML(string xmlInfo);
        string ExportToXML();
        IBaseLayer GetLayerById(string layerid);
        IBaseLayer GetLayerByIndex(int index);
        int GetLayerCount();
        List<IBaseLayer> SelectAllSubLayers();

        // Properties
        bool Expanded { get; set; }
    }
}
