# IDLER (very much a work in progress)
Unity library for mobile idle/incremental games. No Unity Package exists yet, so if you want to use this you'll need to switch over to the IL2CPP scripting backend and .NET 4.x api compat level in your Player settings (to support dynamic types in C#) and include the Unity specific build of Newtonsoft's Json.NET through your package manager (https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git#upm).

### Attributes
Attributes can be anything from data values attached to a character (HP, Stamina, Agility, etc) to data attributes associated with your chicken farm (Egg Value, Hatch Rate, Shipping Rate, etc). See [Creating an Attribute](#creating-an-attribute).

### Modifiers
Modifiers are anything that modify Attributes. Standard Modifier types are Upgrades (lost on prestige), PersistentUpgrades (kept on prestige) and Equipment (applied when equipped), and are meant to be purchased through a store feature of some kind (future feature) using an Attribute as currency (be it money, XP, what have you). Creating Modifiers is similar enough to creating Attributes that a separate section isn't needed.<br><br>\*\*Equipment slots and inventory are coming soon. In the meantime, all equipment given to the character is applied automatically.

### Characters
Characters have Attributes and Modifiers. Characters apply all of their Modifiers each frame in order to get final Attribute values for that frame. To create a Character, create a new class and extend the CharacterBase class. Use `void ApplyAttribute(string resourceId)` and simliar methods to give your Character Attributes and Modifiers.

### ResourceLibrary
A ResourceLibrary singleton is used to deserialize these data objects into template types, then create and deliver Attributes and Modifiers upon a Character's request using these templates. The ResourceLibrary dual-use, acting as an Attribute/Modifier factory and a game string library detailed further below.<br><br>\*\*A GameObject with the ResourceLibrary component __must__ be in the scene for Idler to work.

### ResourceIds
All data resources such as Attributes and Modifiers are meant to be associated with a string ID within the static ResourceIds class. Resources are then accessed and requested in various ways using these resource ids.

### Localized game strings
A Localization feature has been included. Localized game strings are deserialized and served via the ResourceLibrary. ResourceIds must be created for these strings. This file must currently be located in a Resources/Localization folder anywhere in the project. It currently resides at Idler/Resources/Localization/LocStrings.json. This will likely change to be more flexible in the near future. Please provide game strings in the JSON file in the following format:
<pre><code>
[
  {
    "id": "EggLayingRate",
    "strings":
      {
      "en": "Egg Laying Rate",
      "fr": "PLACEHOLDER",
      "it": "PLACEHOLDER",
      "de": "PLACEHOLDER",
      "es": "PLACEHOLDER"
    }
  },
  {
    "id": "EggValue",
    "strings":
    {
      "en": "Egg Value",
      "fr": "PLACEHOLDER",
      "it": "PLACEHOLDER",
      "de": "PLACEHOLDER",
      "es": "PLACEHOLDER"
    }
  }
]
</code></pre>

### Creating an Attribute <a name="creating-an-attribute"></a>
1. Open Idler/Resources/ResourceIds and add a const string id for your Attribute.
<pre><code>
public static class ResourceIds
{
  public const string ShipManufactureRate = "ShipManufactureRate";
  ...
}
</code></pre>

2. Right click in Unity's Project window and select Create -> ScriptableObjects -> Attribute.
3. Fill out the Attribute information. Select your Attribute's Resource ID in the dropdown. This will be used to identify your Attribute and to retrieve its name string.
4. Move the Attribute object to a Resources/Attributes folder anywhere in the project. This will likely change in the near future to be more flexible.