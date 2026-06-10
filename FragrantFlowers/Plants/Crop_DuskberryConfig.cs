using System;
using TUNING;
using UnityEngine;
using STR = STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace FragrantFlowers
{
    internal class Crop_DuskberryConfig : IEntityConfig
    {
        public string[] GetDlcIds() => DlcManager.EXPANSION1;

        public const string ID = "Duskberry";
        public const float GROW_TIME = 12 * 600f;


        public GameObject CreatePrefab()
        {
            GameObject template = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.CROPS.DUSKBERRY.NAME,
                STRINGS.CROPS.DUSKBERRY.DESC,
                1f,
                false,
                Assets.GetAnim("fruit_duskberry_kanim"),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true,
                0,
                SimHashes.Creature,
                null);

            EdiblesManager.FoodInfo foodInfo = new EdiblesManager.FoodInfo(
                ID, 1000000f, 0, 255.15f, 277.15f, 4800f, true, DlcManager.EXPANSION1);

            //ExpandBerrySludgeRecipe();

            return EntityTemplates.ExtendEntityToFood(template, foodInfo);
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        private void ExpandBerrySludgeRecipe()
        {

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement((Tag) ColdWheatConfig.SEED_ID, 5f),
                new ComplexRecipe.RecipeElement((Tag) ID, 1.6f)
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement(FruitCakeConfig.ID.ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            FruitCakeConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(MicrobeMusherConfig.ID, ingredients, results), ingredients, results)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = STR.ITEMS.FOOD.FRUITCAKE.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { MicrobeMusherConfig.ID },
                sortOrder = 3
            };
        }
    }
}
