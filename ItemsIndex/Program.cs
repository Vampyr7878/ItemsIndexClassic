using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ItemsIndex
{
    class Program
    {
        static string path = @"C:\Users\wojte\Documents\Visual Studio 2013\Projects\WoW Character Viewer Classic\WoW Character Viewer Classic\Data\";
        static XmlSerializer serializer;
        static Index index;

        static void Main(string[] args)
        {
            serializer = new XmlSerializer(typeof(Items));
            using(StreamWriter writer = new StreamWriter(path + "ItemsIndex.xml"))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                writer.WriteLine("<Index>");
                AddItems("NeckItems.xml", writer);
                AddItems("FingerItems.xml", writer);
                AddItems("TrinketItems.xml", writer);
                AddItems("BackItems.xml", writer);
                AddItems("ClothChestItems.xml", writer);
                AddItems("LeatherChestItems.xml", writer);
                AddItems("MailChestItems.xml", writer);
                AddItems("PlateChestItems.xml", writer);
                AddItems("ClothWristItems.xml", writer);
                AddItems("LeatherWristItems.xml", writer);
                AddItems("MailWristItems.xml", writer);
                AddItems("PlateWristItems.xml", writer);
                AddItems("ClothHandsItems.xml", writer);
                AddItems("LeatherHandsItems.xml", writer);
                AddItems("MailHandsItems.xml", writer);
                AddItems("PlateHandsItems.xml", writer);
                AddItems("ClothWaistItems.xml", writer);
                AddItems("LeatherWaistItems.xml", writer);
                AddItems("MailWaistItems.xml", writer);
                AddItems("PlateWaistItems.xml", writer);
                AddItems("ClothLegsItems.xml", writer);
                AddItems("LeatherLegsItems.xml", writer);
                AddItems("MailLegsItems.xml", writer);
                AddItems("PlateLegsItems.xml", writer);
                AddItems("ClothFeetItems.xml", writer);
                AddItems("LeatherFeetItems.xml", writer);
                AddItems("MailFeetItems.xml", writer);
                AddItems("PlateFeetItems.xml", writer);
                AddItems("ShirtItems.xml", writer);
                AddItems("TabardItems.xml", writer);
                AddItems("ClothHeadItems.xml", writer);
                AddItems("LeatherHeadItems.xml", writer);
                AddItems("MailHeadItems.xml", writer);
                AddItems("PlateHeadItems.xml", writer);
                AddItems("ClothShoulderItems.xml", writer);
                AddItems("LeatherShoulderItems.xml", writer);
                AddItems("MailShoulderItems.xml", writer);
                AddItems("PlateShoulderItems.xml", writer);
                AddItems("DaggerItems.xml", writer);
                AddItems("FistWeaponItems.xml", writer);
                AddItems("OneHandedAxeItems.xml", writer);
                AddItems("OneHandedMaceItems.xml", writer);
                AddItems("OneHandedSwordItems.xml", writer);
                AddItems("PolearmItems.xml", writer);
                AddItems("StaffItems.xml", writer);
                AddItems("TwoHandedAxeItems.xml", writer);
                AddItems("TwoHandedMaceItems.xml", writer);
                AddItems("TwoHandedSwordItems.xml", writer);
                AddItems("BowItems.xml", writer);
                AddItems("CrossbowItems.xml", writer);
                AddItems("GunItems.xml", writer);
                AddItems("ThrownItems.xml", writer);
                AddItems("WandItems.xml", writer);
                AddItems("ShieldItems.xml", writer);
                AddItems("HeldInOffHandItems.xml", writer);
                AddItems("IdolItems.xml", writer);
                AddItems("LibramItems.xml", writer);
                AddItems("TotemItems.xml", writer);
                AddItems("ArrowItems.xml", writer);
                AddItems("BulletItems.xml", writer);
                AddItems("ReagentItems.xml", writer);
                AddItems("BagItems.xml", writer);
                AddItems("MountItems.xml", writer);
                writer.WriteLine("</Index>");
            }
            serializer = new XmlSerializer(typeof(Index));
            using(StreamReader reader = new StreamReader(path + "ItemsIndex.xml"))
            {
                index = (Index)serializer.Deserialize(reader);
            }
            SortItems();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t"
            };
            using(StreamWriter writer = new StreamWriter(path + "ItemsIndex.xml"))
            {
                using(XmlWriter xml = XmlWriter.Create(writer, settings))
                {
                    serializer.Serialize(xml, index);
                }
            }
        }

        static void AddItems(string file, StreamWriter writer)
        {
            Items items;
            using(StreamReader reader = new StreamReader(path + file))
            {
                items = (Items)serializer.Deserialize(reader);
            }
            foreach(ItemsItem item in items.Item)
            {
                writer.WriteLine("\t<Item id=\"" + item.ID + "\" file=\"" + file + "\"/>");
            }
        }

        static void SortItems()
        {
            List<IndexItem> items = new List<IndexItem>(index.Item);
            List<IndexItem> special = new List<IndexItem>();
            int id;
            foreach(IndexItem item in items)
            {
                if(!int.TryParse(item.id, out id))
                {
                    special.Add(item);
                }
            }
            foreach(IndexItem item in special)
            {
                items.Remove(item);
            }
            items.Sort((x, y) => int.Parse(x.id).CompareTo(int.Parse(y.id)));
            special.AddRange(items);
            index.Item = special.ToArray();
        }
    }
}
