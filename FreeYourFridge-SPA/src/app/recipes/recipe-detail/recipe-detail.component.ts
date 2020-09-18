import { Component, OnInit, Input } from '@angular/core';
import { RecipeToDetails } from '../../_models/recipeToDetails';
import { Nutritions } from '../../_models/recipeNutritions';
import { Instruction } from '../../_models/instruction';
import { MealDto } from '../../_models/mealDto';
import { IngredientDto } from '../../_models/ingredientDto';
import { RecipeIngredients } from '../../_models/ingredient';
import { RecipeService } from '../../_services/recipe.service';
import { AlertifyjsService } from '../../_services/alertifyjs.service';
import { ActivatedRoute } from '@angular/router';
import { DealMealService } from 'src/app/_services/dealMeal.service';
import { Data } from '../../data';
import { RecipeToList } from 'src/app/_models/recipeToList';
import { Router } from '@angular/router';
import { ShoppingListService } from 'src/app/_services/shoppingList.service';
import { ToDoItem } from 'src/app/_models/toDoItem';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss'],
})
export class RecipeDetailComponent implements OnInit {
  recipeToDetail: RecipeToDetails;
  nutritions: Nutritions;
  instructions: Instruction[];
  recipeIngredients: RecipeIngredients;
  slideHtml;
  recipeToList: RecipeToList;
  model: any = {};
  mealDto: MealDto;
  text = 'Shop';

  constructor(
    private recipeService: RecipeService,
    private alertify: AlertifyjsService,
    private route: ActivatedRoute,
    private dealMeal: DealMealService,
    private shoppingList: ShoppingListService,
    private data: Data,
    private routeDirection: Router
  ) {}

  ngOnInit() {
    this.loadRecipe();
    this.loadNutritions();
    this.loadInstruction();
    this.loadIngredient();
  }

  loadRecipe() {
    this.recipeService.getRecipe(+this.route.snapshot.params['id']).subscribe(
      (recipeToDetail: RecipeToDetails) => {
        this.recipeToDetail = recipeToDetail;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  loadNutritions() {
    this.recipeService
      .getNutrition(+this.route.snapshot.params['id'])
      .subscribe(
        (nutritions: Nutritions) => {
          this.nutritions = nutritions;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  loadInstruction() {
    this.recipeService
      .getInstruction(+this.route.snapshot.params['id'])
      .subscribe(
        (instructions: Instruction[]) => {
          this.instructions = instructions;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  loadIngredient() {
    this.recipeToList = this.data.storage;
    console.log(this.recipeToList);
  }

  addMeal(meal) {
    const mealDto = new MealDto();
    mealDto.Grams = +meal.grams;
    mealDto.spoonacularId = +meal.spoonacularId;
    this.dealMeal.addMeal(mealDto).subscribe(
      () => {
        this.alertify.success('Meal added');
      },
      (error) => {
        this.alertify.error('Some problem occur');
        this.model.username = '';
        this.model.password = '';
      }
    );
    this.routeDirection.navigate(['/dailyMeal']);
  }

  addIngredientToShoppingList(ingredient) {
    const toDoItem = new ToDoItem();
    toDoItem.spoonacularId = +ingredient.id;
    toDoItem.amount = +ingredient.amount;
    toDoItem.name = ingredient.name;
    toDoItem.unit = ingredient.unit;

    this.shoppingList.addToDoItem(toDoItem).subscribe(
      () => {
        this.alertify.success('Added to shoplist');
      },
      (error) => {
        this.alertify.error('Some problem occur');
      }
    );
    // this.routeDirection.navigate(['/dailyMeal']);
  }

  removeIngredientFromShoppingList(ingredient){
    const toDoItem = new ToDoItem();
    toDoItem.spoonacularId = +ingredient.id;
    toDoItem.amount = +ingredient.amount;
    toDoItem.name = ingredient.name;
    toDoItem.unit = ingredient.unit;


    this.shoppingList.deleteToDoItems(toDoItem.spoonacularId).subscribe(
      () => {
        this.alertify.success('Removed to shoplist');
      },
      (error) => {
        this.alertify.error('Some problem occur');
      }
    );
  }

  private toggleMe(ingredient): void {
    const element = document.getElementById("btn-"+ingredient.id).innerHTML = document.getElementById("btn-"+ingredient.id).innerHTML == "Remove" ? "Shop" : "Remove";
    document.getElementById("btn-"+ingredient.id).classList.toggle("red");
    const isRed = document.getElementById("btn-"+ingredient.id).classList.contains("red");
   isRed? this.addIngredientToShoppingList(ingredient):this.removeIngredientFromShoppingList(ingredient)

  }
}
