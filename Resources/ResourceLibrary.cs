//=============================================================================
// ResourceLibrary.cs
//
// A loader/factory/library singleton class for all Idler resources.
//=============================================================================

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Idler.Resources
{
    //=========================================================================
    // ResourceLibrary
    //=========================================================================
    public class ResourceLibrary : Core.Singleton<ResourceLibrary>
    {
        private Dictionary<string, Attributes.Templates.AttributeTemplate> _attributeTemplates;
        private Dictionary<string, Modifiers.Templates.UpgradeTemplate> _upgradeTemplates;
        private Dictionary<string, Modifiers.Templates.PersistentUpgradeTemplate> _persistentUpgradeTemplates;
        private Dictionary<string, Modifiers.Templates.EquipmentTemplate> _equipmentTemplates;
        private Dictionary<string, LocStringResource> _locStringResources;

        //=========================================================================
        //=========================================================================
        protected override void Awake()
        {
            base.Awake();
            _attributeTemplates = new Dictionary<string, Attributes.Templates.AttributeTemplate>();
            _upgradeTemplates = new Dictionary<string, Modifiers.Templates.UpgradeTemplate>();
            _persistentUpgradeTemplates = new Dictionary<string, Modifiers.Templates.PersistentUpgradeTemplate>();
            _equipmentTemplates = new Dictionary<string, Modifiers.Templates.EquipmentTemplate>();
            _locStringResources = new Dictionary<string, LocStringResource>();
            
            // Load LocStrings into memory
            InitLocStrings();
            
            // TODO: Allow attribute/modifier files to be stored anywhere by using OS file loading and custom deserializer
            
            // Deserialize attributes
            _attributeTemplates =
                UnityEngine.Resources.LoadAll<Attributes.Templates.AttributeTemplate>("Attributes")
                    .ToDictionary(template => template.resourceId, template => template);
            
            // Deserialize upgrade templates
            _upgradeTemplates =
                UnityEngine.Resources.LoadAll<Modifiers.Templates.UpgradeTemplate>("Upgrades")
                    .ToDictionary(template => template.resourceId, template => template);


            // Deserialize persistent upgrade templates
            _persistentUpgradeTemplates =
                UnityEngine.Resources.LoadAll<Modifiers.Templates.PersistentUpgradeTemplate>("PersistentUpgrades")
                    .ToDictionary(template => template.resourceId, template => template);


            // Deserialize equipment templates
            _equipmentTemplates = 
                UnityEngine.Resources.LoadAll<Modifiers.Templates.EquipmentTemplate>("Equipment")
                    .ToDictionary(template => template.resourceId, template => template);
        }

        //=========================================================================
        //=========================================================================
        public Attributes.Attribute CreateAttribute(string rid)
        {
            Assert.IsTrue(_attributeTemplates.ContainsKey(rid),
                $"CreateAttribute: Attribute '{rid}' not found.");

            return new Attributes.Attribute(_attributeTemplates[rid]);
        }

        //=========================================================================
        //=========================================================================
        public Modifiers.Upgrade CreateUpgrade(string rid)
        {
            Assert.IsTrue(_upgradeTemplates.ContainsKey(rid),
                $"CreateUpgrade: Upgrade '{rid}' not found.");

            return new Modifiers.Upgrade(_upgradeTemplates[rid]);
        }

        //=========================================================================
        //=========================================================================
        public Modifiers.PersistentUpgrade CreatePersistentUpgrade(string rid)
        {
            Assert.IsTrue(_persistentUpgradeTemplates.ContainsKey(rid),
                $"CreatePersistentUpgrade: PersistentUpgrade '{rid}' not found.");

            return new Modifiers.PersistentUpgrade(_persistentUpgradeTemplates[rid]);
        }

        //=========================================================================
        //=========================================================================
        public Modifiers.Equipment CreateEquipment(string rid)
        {
            Assert.IsTrue(_equipmentTemplates.ContainsKey(rid),
                $"CreateEquipment: Equipment '{rid}' not found.");

            return new Modifiers.Equipment(_equipmentTemplates[rid]);
        }
        
        //=========================================================================
        //=========================================================================
        public string GetLocString(string rid)
        {
            Assert.IsTrue(_locStringResources.ContainsKey(rid), $"GetLocString: Resource Id '{rid}' not found.");
            return _locStringResources[rid];
        }

        //=========================================================================
        //=========================================================================
        private void InitLocStrings()
        {
            _locStringResources = new Dictionary<string, LocStringResource>();

            // Load/deserialize json file resource
            //
            const string filepath = "Localization/LocStrings";
            var jsonString = UnityEngine.Resources.Load<TextAsset>(filepath).text;

            dynamic jObject = JsonConvert.DeserializeObject(jsonString);
            if (jObject == null)
            {
                throw new JsonException("Failed to load LocStrings.json file");
            }

            var jArray = (JArray) jObject;
            if (jArray[0] == null)
            {
                throw new JsonException("LocStrings file is empty");
            }

            for (var i = 0; i < jArray.Count; ++i)
            {
                if (jObject[i].id == null)
                {
                    throw new JsonException($"Missing 'id' field in element {i+1} of LocStrings.json");
                }

                var id = (string) jObject[i].id;

                if (jObject[i].strings == null)
                {
                    throw new JsonException($"Missing 'strings' field in '{id}' in LocStrings.json");
                }

                if (jObject[i].strings.en == null || jObject[i].strings.fr == null ||
                    jObject[i].strings.it == null || jObject[i].strings.de == null ||
                    jObject[i].strings.es == null)
                {
                    throw new JsonException($"Missing language field in '{id}.strings' in LocStrings.json");
                }

                var stringResource = new LocStringResource
                {
                    ["en"] = (string) jObject[i].strings.en,
                    ["fr"] = (string) jObject[i].strings.fr,
                    ["it"] = (string) jObject[i].strings.it,
                    ["de"] = (string) jObject[i].strings.de,
                    ["es"] = (string) jObject[i].strings.es,
                };

                _locStringResources.Add(id, stringResource);
            }
        }
    }
    
    //=========================================================================
    // LocStringResource
    //=========================================================================
    public class LocStringResource
    {
        private readonly Dictionary<string, string> _stringCollection;

        //=========================================================================
        //=========================================================================
        public LocStringResource()
        {
            _stringCollection = new Dictionary<string, string>();
        }

        //=========================================================================
        //=========================================================================
        public string this[string str]
        {
            get
            {
                Assert.IsTrue(_stringCollection.ContainsKey(str), $"StringResource[get]: Invalid key '{str}'.");
                return _stringCollection[str];
            }
            set => _stringCollection[str] = value;
        }
        
        //=========================================================================
        //=========================================================================
        public static implicit operator string(LocStringResource r)
        {
            return r[Localization.Language];
        }
    }
}
