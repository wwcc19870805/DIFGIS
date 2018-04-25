using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using DFWinForms.Component;
using DevExpress.XtraBars;
using DFWinForms.Class;
using DevExpress.XtraBars.Ribbon;
using DFWinForms.Doozer;

namespace DFWinForms.Service
{
    public static class MenuService
    {
        private static bool isContextMenuOpen;
        public static bool IsContextMenuOpen
        {
            get
            {
                return MenuService.isContextMenuOpen;
            }
        }
        public static void AddQuickToolBar(System.Collections.IList collection1, System.Collections.IList collection2)
        {
            collection2.Clear();
            foreach (object obj1 in collection1)
            {
                if(!(obj1 is BarItem)) continue;
                BarItem bi = obj1 as BarItem;
                if (AddInTree.QuickToolbarItem.Contains(bi.Name))
                {
                    bool flag = false;
                    foreach (object obj2 in collection2)
                    {
                        if(!(obj2 is BarItemLink)) continue;
                        BarItemLink bil = obj2 as BarItemLink;
                        if (bil.Item.Name == bi.Name)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        collection2.Add(obj1);
                    }
                }
            }
        }
        public static void AddApplicationMenu(System.Collections.IList collection, object owner, string addInTreePath)
        {
            MenuService.AddItemsToApplicationMenu(collection, AddInTree.BuildItems<MenuItemDescriptor>(addInTreePath, owner, false));
        }
        private static void AddItemsToApplicationMenu(System.Collections.IList collection, System.Collections.Generic.List<MenuItemDescriptor> descriptors)
        {
            foreach (MenuItemDescriptor current in descriptors)
            {
                object obj = MenuService.CreateMenuItemFromDescriptor(current);
                if (obj is MenuButtonCommand)
                {
                    MenuService.Add(collection, (MenuButtonCommand)obj);
                }
            }
        }
        public static void AddItemsToMenu(System.Collections.IList collection, object owner, string addInTreePath)
        {
            MenuService.AddItemsToMenu(collection, AddInTree.BuildItems<MenuItemDescriptor>(addInTreePath, owner, false));
        }
        private static void AddItemsToMenu(System.Collections.IList collection, System.Collections.Generic.List<MenuItemDescriptor> descriptors)
        {
            foreach (MenuItemDescriptor current in descriptors)
            {
                object obj = MenuService.CreateMenuItemFromDescriptor(current);
                if (obj is MenuRibbonPageCategory)
                {
                    MenuService.Add(collection, obj);
                    if (current.SubItems != null)
                    {
                        MenuService.AddItemsToMenu(((MenuRibbonPageCategory)obj).Pages, MenuService.ToList(current.SubItems));
                    }
                }
                else if (obj is MenuRibbonPage)
                {
                    MenuService.Add(collection, obj);
                    if (current.SubItems != null)
                    {
                        MenuService.AddItemsToMenu(((MenuRibbonPage)obj).Groups, MenuService.ToList(current.SubItems));
                    }
                }
                else
                {
                    if (obj is MenuRibbonPageGroup)
                    {
                        MenuRibbonPageGroup group = (MenuRibbonPageGroup)obj;
                        if (group != null)
                        {
                            Add(collection, group);
                            if (current.SubItems != null)
                            {
                                AddItemsToMenu(((MenuRibbonPageGroup)obj).ItemLinks, ToList(current.SubItems));
                            }
                        }
                        continue;
                    }
                    if (obj is MenuSubItemCommand)
                    {
                        Add(collection, obj);
                        ((MenuSubItemCommand)obj).CreateItems();
                        continue;
                    }
                    //if (obj is MenuButtonGroupCommand)
                    //{
                    //    MenuService.Add(collection, obj);
                    //    if (current.SubItems != null)
                    //    {
                    //        MenuService.AddItemsToMenu(((MenuButtonGroupCommand)obj).ItemLinks, MenuService.ToList(current.SubItems));
                    //    }
                    //}
                    if (obj is BarItem)
                    {
                        Add(collection, obj);
                        if (obj is IStatusUpdate)
                        {
                            ((IStatusUpdate)obj).UpdateStatus();
                            ((IStatusUpdate)obj).UpdateText();
                        }
                        continue;
                    }
                    if (obj is ContextMenuCommand)
                    {
                        ContextMenuCommand command2 = obj as ContextMenuCommand;
                        if (command2 != null)
                        {
                            Add(collection, command2);
                        }
                    }                
                  
                }
            }
        }
        private static object CreateMenuItemFromDescriptor(MenuItemDescriptor descriptor)
        {
            Codon codon = descriptor.Codon;
            string text = codon.Properties.Contains("type") ? codon.Properties["type"] : "Command";
            bool createCommand = codon.Properties["loadclasslazy"] == "false";
            string key;
            switch (key = text)
            {
                case "RibbonPageCategory":
                    {
                        return new MenuRibbonPageCategory(codon, descriptor.Caller, MenuService.ConvertSubItems(descriptor.SubItems));
                    }
                case "RibbonPage":
                    {
                        return new MenuRibbonPage(codon, descriptor.Caller, MenuService.ConvertSubItems(descriptor.SubItems));
                    }
                case "RibbonPageGroup":
                    {
                        return new MenuRibbonPageGroup(codon, descriptor.Caller, MenuService.ConvertSubItems(descriptor.SubItems));
                    }
                case "SubItemCommand":
                    {
                        return new MenuSubItemCommand(codon, descriptor.Caller, MenuService.ConvertSubItems(descriptor.SubItems));
                    }
                case "ButtonGroupCommand":
                    {
                        return new MenuButtonGroupCommand(codon, descriptor.Caller, MenuService.ConvertSubItems(descriptor.SubItems));
                    }
                case "ContextMenuCommand":
                    {
                        return new ContextMenuCommand(codon, descriptor.Caller, MenuService.ConvertSubItems(descriptor.SubItems));
                    }
                case "RadioCommand":
                case "CheckCommand":
                    {
                        return new MenuCheckCommand(codon, descriptor.Caller, createCommand);
                    }
                case "Command":
                case "ButtonCommand":
                    {
                        return new MenuButtonCommand(codon, descriptor.Caller, createCommand);
                    }
                case "CheckBoxCommand":
                    {
                        return new MenuCheckBoxCommand(codon, descriptor.Caller, createCommand);
                    }
                case "ComboBoxCommand":
                    {
                        return new MenuComboBoxCommand(codon, descriptor.Caller, createCommand);
                    }
                case "SpinCommand":
                    {
                        return new MenuSpinCommand(codon, descriptor.Caller, createCommand);
                    }
                case "TextCommand":
                    {
                        return new MenuTextCommand(codon, descriptor.Caller, createCommand);
                    }
                case "MenuTrackBarCommand":
                    {
                        return new MenuTrackBarCommand(codon, descriptor.Caller, createCommand);
                    }
                case "Builder":
                    {
                        return codon.AddIn.CreateObject(codon.Properties["class"]);
                    }
            }
            throw new System.NotSupportedException("unsupported menu item type : " + text);
        }
        public static void UpdateMenu(System.Collections.ICollection bars)
        {
            foreach (object current in bars)
            {
                if (current is IStatusUpdate)
                {
                    ((IStatusUpdate)current).UpdateStatus();
                }
            }
        }
        internal static System.Collections.ArrayList ConvertSubItems(System.Collections.IList items)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            if (items != null)
            {
                foreach (MenuItemDescriptor descriptor in items)
                {
                    arrayList.Add(MenuService.CreateMenuItemFromDescriptor(descriptor));
                }
            }
            return arrayList;
        }
        internal static System.Collections.Generic.List<MenuItemDescriptor> ToList(System.Collections.IList itemList)
        {
            System.Collections.Generic.List<MenuItemDescriptor> list = new System.Collections.Generic.List<MenuItemDescriptor>();
            foreach (MenuItemDescriptor item in itemList)
            {
                list.Add(item);
            }
            return list;
        }
        internal static void Add(System.Collections.IList collection, object item)
        {
            if (collection != null)
            {
                collection.Add(item);
            }
            if (item is IStatusUpdate)
            {
                ((IStatusUpdate)item).UpdateStatus();
            }
            //if (item is MenuRibbonPageGroup)
            //{
            //    MenuRibbonPageGroup temp = item as MenuRibbonPageGroup;
            //    if (!temp.Visible)
            //    {
            //        return;
            //    }
            //}
            //else if (item is MenuRibbonPage)
            //{
            //    MenuRibbonPage temp = item as MenuRibbonPage;
            //    if (!temp.Visible) return;
            //}
            //else if (item is MenuRibbonPageCategory)
            //{
            //    MenuRibbonPageCategory temp = item as MenuRibbonPageCategory;
            //    if (!temp.Visible) return;
            //}
        }
        public static System.Windows.Forms.ContextMenuStrip CreateContextMenu(object owner, string addInTreePath)
        {
            if (string.IsNullOrEmpty(addInTreePath))
            {
                return null;
            }
            System.Windows.Forms.ContextMenuStrip result;
            try
            {
                System.Collections.Generic.List<MenuItemDescriptor> destriptors = AddInTree.BuildItems<MenuItemDescriptor>(addInTreePath, owner, true);
                System.Windows.Forms.ContextMenuStrip menuStrip = new System.Windows.Forms.ContextMenuStrip();
                menuStrip.Opening += delegate
                {
                    menuStrip.Items.Clear();
                    MenuService.AddItemsToMenu(menuStrip.Items, destriptors);
                }
                ;
                result = menuStrip;
            }
            catch (System.Exception)
            {
                result = null;
            }
            return result;
        }
        private static void MenuStripClosed(object sender, System.EventArgs e)
        {
            MenuService.isContextMenuOpen = false;
        }
        private static void MenuStripOpened(object sender, System.EventArgs e)
        {
            MenuService.isContextMenuOpen = true;
            System.Windows.Forms.ContextMenuStrip contextMenuStrip = sender as System.Windows.Forms.ContextMenuStrip;
            foreach (object current in contextMenuStrip.Items)
            {
                if (current is IStatusUpdate)
                {
                    ((IStatusUpdate)current).UpdateStatus();
                }
            }
        }
        public static void ShowContextMenu(object owner, string addInTreePath, System.Windows.Forms.Control parent, int x, int y)
        {
            System.Windows.Forms.ContextMenuStrip contextMenuStrip = MenuService.CreateContextMenu(owner, addInTreePath);
            if (contextMenuStrip != null)
            {
                contextMenuStrip.Show(parent, new System.Drawing.Point(x, y));
            }
        }
        public static void AddDockPanelMenu(RibbonPageCollection pages, object owner, string addInTreePath)
        {
            System.Collections.Generic.List<DockPanelDescriptor> list = AddInTree.BuildItems<DockPanelDescriptor>(addInTreePath, owner, false);
            foreach (DockPanelDescriptor current in list)
            {
                MenuDockPanelCommand menuDockPanelCommand = new MenuDockPanelCommand(current.Codon, owner, true);
                string[] array = menuDockPanelCommand.Category.Split(new char[]
				{
					'|'
				});
                if (pages != null)
                {
                    RibbonPage pageByName = pages.GetPageByName(array[0]);
                    if (pageByName != null)
                    {
                        RibbonPageGroup groupByName = pageByName.Groups.GetGroupByName(array[1]);
                        if (groupByName != null)
                        {
                            groupByName.ItemLinks.Add(menuDockPanelCommand);
                        }
                    }
                }
            }
        }

        public static void SetToolBarsInvisible(System.Collections.ICollection toolBars)
        {
            foreach (object obj2 in toolBars)
            {
                if (!(obj2 is BarItemLink)) continue;
                BarItemLink bil = obj2 as BarItemLink;
                bil.Visible = false;
            }
        }
        public static void SwitchToMenu(System.Collections.ICollection menus,System.Collections.ICollection toolBars, string[] addInTreePaths)
        {
            foreach (object obj in menus)
            {
                if (obj is RibbonPage)
                {
                    RibbonPage page = obj as RibbonPage;
                    bool bHave = false;
                    foreach (string addInTreePath in addInTreePaths)
                    {
                        if (AddInTree.ExistsTreeNode(addInTreePath + "/" + page.Name))
                        {
                            page.Visible = true;
                            if (page.Text.Contains("temp"))
                                page.Text = page.Text.Substring(0, page.Text.Count() - 4);
                            bHave = true;
                            break;
                        }
                    }
                    if (!bHave)
                    {
                        page.Visible = false;
                        if (!page.Text.Contains("temp"))
                            page.Text = page.Text + "temp";
                    }
                    SwitchToMenu(page.Groups, toolBars, addInTreePaths);
                }
                else if (obj is RibbonPageGroup)
                {
                    RibbonPageGroup pageGroup = obj as RibbonPageGroup;
                    bool bHave = false;
                    foreach (string addInTreePath in addInTreePaths)
                    {
                        if (AddInTree.ExistsTreeNode(addInTreePath + "/" + pageGroup.Page.Name + "/" + pageGroup.Name))
                        {
                            pageGroup.Visible = true;
                            foreach (object obj2 in toolBars)
                            {
                                if (!(obj2 is BarItemLink)) continue;
                                BarItemLink bil = obj2 as BarItemLink;
                                for (int i = 0; i < pageGroup.ItemLinks.Count;i++ )
                                {
                                    BarItemLink itemLink = pageGroup.ItemLinks.GetRecentLink(i);
                                    if (itemLink.Item.Name == bil.Item.Name)
                                    {
                                        bil.Visible = true;
                                        break;
                                    }
                                }
                            }
                            bHave = true;
                            break;
                        }
                    }
                    if (!bHave) pageGroup.Visible = false;
                }                
            }
        }
    }
}
