
# Minecraft Crafting Calculator

A desktop application that loads official Minecraft recipe files and calculates the total raw materials needed for any custom build plan. This project was built as a technical portfolio project to demonstrate WinForms GUI development, recursive data modeling, and JSON parsing for real-world data.

---

## 🧩 Features

- **Recipe Loading:** Load all official Minecraft recipe files directly from the game JAR (see setup instructions below).
- **Multi-Item Crafting Plans:** Input multiple items with quantity requirements and calculate total raw ingredient needs.
- **Recipe Types Supported:**
  - Crafting (shaped & shapeless)
  - Smelting
  - Blasting
  - Smoking
  - Campfire Cooking
  - Stonecutting
- **Autocomplete Input:** Auto-suggests valid item names based on loaded data.
- **Error-Resilient Parsing:** Handles modded or malformed recipes gracefully with detailed error reporting.
- **Expandable:** Ready for future features like recursive breakdown trees and plan exporting.

---

## 🚀 Setup & Usage


### 1. Extract Minecraft recipe JSON files

1. Navigate to your `.minecraft/versions/<version>/` folder.  
   (e.g. `.minecraft/versions/1.20.4/`)
2. Rename `1.20.4.jar` to `1.20.4.zip`
3. Extract it using any zip tool.
4. Inside the extracted folder, go to:  
   `data/minecraft/recipes`
5. Select and load all `.json` recipe files into the app using the **"Load Recipes"** button.

> 💡 Tip: You can also batch load recipes from modded Minecraft JARs using the same method.

### 2. Run the application

1. Download the latest release from the [Releases](https://github.com/toby-sym/Minecraft-Crafting-Calculator/releases) page.
2. Extract the `.zip` file.
3. Launch `Minecraft-Crafting-Calculator`.
4. Use the GUI to:
   - Load recipes
   - Add items and quantities
   - Calculate total raw material requirements

---

## 🛠️ Tech Stack

- **Language:** C#
- **Framework:** .NET (Windows Forms)
- **UI:** WinForms
- **Libraries:**  
  - [Newtonsoft.Json](https://www.newtonsoft.com/json) for advanced JSON parsing
- **IDE:** Visual Studio

---

## ✅ Advanced Topics Covered

- Parsing complex and inconsistent JSON recipe data (arrays, objects, string fallbacks)
- Dynamic WinForms GUI logic with autocomplete and list binding
- Aggregate computation of nested crafting trees (non-recursive for now)
- Error handling for edge case recipes and missing data
- Mod support through flexible recipe loading

---

## 📦 Planned Features

- Recursive ingredient expansion (e.g. crafting required sub-items)
- Exportable build plans (CSV/JSON)
- Breakdown trees for UI display
- SQLite or file-based save/load system for large builds

---

[@toby-sym](https://github.com/toby-sym)
