﻿using EF_DietaryProject.DAL;
using EF_DietaryProject.Entities.Entities;
using EF_DietaryProject.Entities.EnumEntities;
using EF_DietaryProject.UI.VMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace EF_DietaryProject.UI
{
    public partial class MealProcessesForm : Form
    {
        string username;
        decimal totalCalories = 0;
        private List<DishDetailViewModel> dishDetailViewModels = new List<DishDetailViewModel>();
        private List<DishDetailViewModel> newDishDetailViewModels = new List<DishDetailViewModel>();

        

        public MealProcessesForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void MealProcessesForm_Load(object sender, EventArgs e)
        {
            LoadMealList();

            dgwAddFood.Columns.Add("FoodName", "FoodName");
            dgwAddFood.Columns.Add("Gramme", "Gramme");

            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            
            this.MinimumSize = new System.Drawing.Size(900, 775);
            this.MaximumSize = new System.Drawing.Size(900, 775);

            cmbMeals.DataSource = UnitOfWork.Instance.MealRepository.GetAll();
            cmbMeals.DisplayMember = "Name";
            cmbMeals.ValueMember = "Id";

            cmbFoodCategory.DataSource = UnitOfWork.Instance.FoodCategoryRepository.GetAll();
            cmbFoodCategory.DisplayMember = "Name";
            cmbFoodCategory.ValueMember = "Id";

            List<Dish> dishes = UnitOfWork.Instance.DishRepository.GetAll().
                Where(x => x.MealDate.Date == dtpMealDay.Value.Date).ToList();
            List<DishViewModel> dishViewModels = GetDishViewModels(dishes);

            dgwDishes.DataSource = dishViewModels;
            dgwDishes.Columns["Id"].Visible = false;
        }

        private void LoadMealList()
        {
            var foods = UnitOfWork.Instance.FoodRepository.GetAll();

            int yPosition = 10;

            foreach (var food in foods)
            {
                
                RadioButton radioButton = new RadioButton();
                radioButton.Text = food.FoodName;
                radioButton.AutoSize = true;
                radioButton.Location = new System.Drawing.Point(10, yPosition);
                radioButton.CheckedChanged += (s, e) =>
                {
                    txtFoodName.Text = food.FoodName;
                    txtCaloryPer100g.Text = (food.CaloryPerGram * 100).ToString();
                };

                radioButton.Tag = food.Id;

                
                PictureBox pictureBox = new PictureBox();
                pictureBox.ImageLocation = food.PhotoPath;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Size = new System.Drawing.Size(200, 200);
                pictureBox.Location = new System.Drawing.Point(20, yPosition);

                
                panel1.Controls.Add(radioButton);
                panel1.Controls.Add(pictureBox);

                yPosition += 220;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            RadioButton selectedRadioButton = panel1.Controls.OfType<RadioButton>().
                FirstOrDefault(rb => rb.Checked);

            if (selectedRadioButton != null)
            {
                int foodId = (int)selectedRadioButton.Tag;

                Food selectedFood = UnitOfWork.Instance.FoodRepository.GetAll().
                    FirstOrDefault(x => x.Id == foodId);


                if (selectedFood != null)
                {
                    decimal caloriesForSelectedFood = nudHowMuchGr.Value * 
                        Convert.ToDecimal(selectedFood.CaloryPerGram);

                    DishDetailViewModel dishDetailViewModel = new DishDetailViewModel
                    {
                        FoodName = selectedRadioButton.Text,
                        Gramme = Convert.ToInt16(nudHowMuchGr.Value)
                    };

                    newDishDetailViewModels.Add(dishDetailViewModel);

                    dishDetailViewModels = newDishDetailViewModels;

                    txtTotalCalories.Text = totalCalories.ToString();

                    MessageBox.Show("The food has been added.");

                    
                    dgwAddFood.DataSource = null; 
                    dgwAddFood.DataSource = newDishDetailViewModels;
                    dgwAddFood.Columns["Id"].Visible = false;
                    dgwAddFood.Columns[0].Visible = false;
                    dgwAddFood.Columns[0].ReadOnly = true;
                    dgwAddFood.Columns[1].Visible = false;
                    dgwAddFood.Columns[1].ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Invalid food selected.");
                }

            }
            else
            {
                MessageBox.Show("Please choose a food.");
            }
        }

        private void cmbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cmbFoodCategory.SelectedIndex;

            if (selectedIndex == -1)
            {
                MessageBox.Show("No category selected.");
                return;
            }

            if (cmbFoodCategory.Items[selectedIndex] is FoodCategory selectedCategory)
            {
                int selectedCategoryId = selectedCategory.Id; 
                FilterFoodsByCategory(selectedCategoryId);
            }
            else
            {
                MessageBox.Show("Invalid category selected.");
            }
        }

        private void FilterFoodsByCategory(int selectedCategoryId)
        {
            var foodFeatures = new List<FoodFeature>();
            if (selectedCategoryId == 11)
            {
                foodFeatures = UnitOfWork.Instance.FoodFeatureRepository.GetAll().ToList();
            }
            else
            {
                foodFeatures = UnitOfWork.Instance.FoodFeatureRepository.GetAll().
                    Where(ff => ff.FoodCategoryId == selectedCategoryId).ToList();
            }

            var distinctFoods = foodFeatures.Select(ff => ff.Food).Distinct().ToList();

            panel1.Controls.Clear();

            int yPosition = 10;

            foreach (var food in distinctFoods)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = food.FoodName;
                radioButton.AutoSize = true;
                radioButton.Location = new System.Drawing.Point(10, yPosition);
                radioButton.CheckedChanged += (s, e) =>
                {
                    txtFoodName.Text = food.FoodName;
                    txtCaloryPer100g.Text = (food.CaloryPerGram * 100).ToString();
                };

                radioButton.Tag = food.Id;

                PictureBox pictureBox = new PictureBox();
                pictureBox.ImageLocation = food.PhotoPath;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Size = new System.Drawing.Size(200, 200);
                pictureBox.Location = new System.Drawing.Point(20, yPosition);

                panel1.Controls.Add(radioButton);
                panel1.Controls.Add(pictureBox);

                yPosition += 220;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgwAddFood.SelectedRows.Count > 0)
            {
                int selectedIndex = dgwAddFood.SelectedRows[0].Index;

                int dishDetailViewModelId = Convert.ToInt32(dgwAddFood.Rows[selectedIndex].Cells["Id"]
                    .Value);

                
                if (dgwAddFood.Rows[selectedIndex].Cells[4].Value != null)
                {
                    decimal grams = Convert.ToDecimal(dgwAddFood.Rows[selectedIndex].Cells[3].Value);
                    string foodName = dgwAddFood.Rows[selectedIndex].Cells[4].Value.ToString();

                    var food = UnitOfWork.Instance.FoodRepository.GetAll().
                        FirstOrDefault(f => f.FoodName == foodName);

                    if (food != null)
                    {
                        totalCalories -= (grams * Convert.ToDecimal(food.CaloryPerGram));
                        txtTotalCalories.Text = totalCalories.ToString();
                    }
                }

                
                var removedItem = dishDetailViewModels.
                    FirstOrDefault(dd => dd.Id == dishDetailViewModelId);

                if (removedItem != null)
                {
                    dishDetailViewModels.Remove(removedItem);
                }

                dgwAddFood.DataSource = null;
                dgwAddFood.DataSource = dishDetailViewModels;
                dgwAddFood.Columns["Id"].Visible = false;

                MessageBox.Show("The food has been deleted.");
            }
            else
            {
                MessageBox.Show("Please select a food to delete.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
                if (dgwAddFood.SelectedRows.Count > 0)
                {
                    int selectedIndex = dgwAddFood.SelectedRows[0].Index;

                    
                    decimal newGrams;
                    if (decimal.TryParse(nudHowMuchGr.Value.ToString(), out newGrams))
                    {
                        
                        decimal oldGrams = Convert.ToDecimal(dgwAddFood.Rows[selectedIndex].
                            Cells[3].Value);
                        string foodName = dgwAddFood.Rows[selectedIndex].Cells[4].Value.ToString();

                        
                        var food = UnitOfWork.Instance.FoodRepository.GetAll().
                            FirstOrDefault(f => f.FoodName == foodName);

                        if (food != null)
                        {
                            totalCalories -= (oldGrams * Convert.ToDecimal(food.CaloryPerGram));

                            

                            totalCalories += (newGrams * Convert.ToDecimal(food.CaloryPerGram));

                            txtTotalCalories.Text = totalCalories.ToString();

                            
                            dgwAddFood.Rows[selectedIndex].Cells[3].Value = newGrams.ToString();

                            MessageBox.Show("The food has been updated.");
                        }
                        else
                        {
                            MessageBox.Show("Food not found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid gram value.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a food to update.");
                }
            
            
        }


        private void btnDone_Click(object sender, EventArgs e)
        {

            var user = UnitOfWork.Instance.AppUserRepository.GetAll().
                FirstOrDefault(x => x.Username == username);

            var userDetail = UnitOfWork.Instance.AppUserDetailRepository.GetAll().
                FirstOrDefault(x => x.AppUserId == user.Id);


            
            Dish dish = new Dish
            {
                AppUserDetailId = userDetail.Id, 
                MealId = (int)cmbMeals.SelectedValue, 
                CaloriesTaken = 0, 
                DishDetails = new List<DishDetail>(),
                MealDate = dtpMealDay.Value.Date
            };


            foreach (DataGridViewRow row in dgwAddFood.Rows)
            {
                dgwAddFood.AllowUserToAddRows = false;

                string foodName = Convert.ToString(row.Cells[4].Value); 
                float gramme = Convert.ToSingle(row.Cells[3].Value); 

               
                Food food = UnitOfWork.Instance.FoodRepository.GetAll().
                    FirstOrDefault(f => f.FoodName == foodName);

                if (food != null)
                {
                    int foodId = food.Id;

                    
                    dish.DishDetails.Add(new DishDetail
                    {
                        DishId = dish.Id,
                        Dish = dish,
                        FoodId = foodId,
                        Food = food,
                        Gramme = gramme
                    });

                    
                    dish.CaloriesTaken = dish.DishDetails.
                        Sum(d => d.Food.CaloryPerGram * d.Gramme);
                }
            }

            UnitOfWork.Instance.DishRepository.Add(dish);

            MessageBox.Show("The meal has been added successfully.");

            
            dishDetailViewModels.Clear();
            dgwAddFood.DataSource = null;
            dgwAddFood.DataSource = newDishDetailViewModels;
            

            List<Dish> dishes = UnitOfWork.Instance.DishRepository.GetAll().
                Include(x => x.AppUserDetail).Where(x => x.MealDate.Date
                == dtpMealDay.Value.Date).ToList();

            List<DishViewModel> dishViewModels = GetDishViewModels(dishes);

            dgwDishes.DataSource = dishViewModels;
            dgwDishes.Columns["Id"].Visible = false;
        }

        private void btnAddMeal_Click(object sender, EventArgs e)
        {

        }

        private List<DishViewModel> GetDishViewModels(List<Dish> dishes)
        {
            var user = UnitOfWork.Instance.AppUserRepository.GetAll().
                FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                MessageBox.Show("User not found.");
                return new List<DishViewModel>();
            }

            List<DishViewModel> dishViewModels = new List<DishViewModel>();

            foreach (var dish in dishes.Where(d => d.AppUserDetailId == user.Id))
            {
                DishViewModel viewModel = new DishViewModel
                {
                    Id = dish.Id,
                    MealDate = dish.MealDate,
                    CaloriesTaken = dish.CaloriesTaken,
                    MealName = dish.Meal?.Name 
                };

                dishViewModels.Add(viewModel);
            }

            return dishViewModels;
        }

        private void dtpMealDay_ValueChanged(object sender, EventArgs e)
        {
            var user = UnitOfWork.Instance.AppUserRepository.GetAll().
                FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            List<Dish> dishes = UnitOfWork.Instance.DishRepository.GetAll().
                Where(x => x.MealDate.Date == dtpMealDay.Value.Date &&
                x.AppUserDetail.AppUserId == user.Id).ToList();

            List<DishViewModel> dishViewModels = GetDishViewModels(dishes);

            dgwDishes.DataSource = dishViewModels;
            dgwDishes.Columns["Id"].Visible = false;
        }

        private void dgwDishes_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedDishId = (int)dgwDishes.Rows[e.RowIndex].Cells["Id"].Value;

            List<DishDetail> dishDetails = UnitOfWork.Instance.DishDetailRepository.
                GetAll().Where(x => x.DishId == selectedDishId).ToList();

            dishDetailViewModels = GetDishDetailViewModels(dishDetails);

            dgwAddFood.DataSource = dishDetailViewModels;
            dgwAddFood.Columns["Id"].Visible = false;
            dgwAddFood.Columns[0].Visible = false;
            dgwAddFood.Columns[0].ReadOnly = true;
            dgwAddFood.Columns[1].Visible = false;
            dgwAddFood.Columns[1].ReadOnly = true;
        }

        private List<DishDetailViewModel> GetDishDetailViewModels(List<DishDetail> dishDetails)
        {
            List<DishDetailViewModel> dishDetailViewModels = new List<DishDetailViewModel>();

            foreach (var detail in dishDetails)
            {
                DishDetailViewModel viewModel = new DishDetailViewModel
                {
                    Id = detail.Id,
                    Gramme = detail.Gramme,
                    FoodName = detail.Food?.FoodName 
                };

                dishDetailViewModels.Add(viewModel);
            }

            return dishDetailViewModels;
        }

        private void btnUpdateDish_Click(object sender, EventArgs e)
        {
            if (dgwAddFood.DataSource == null || dishDetailViewModels == null)
            {
                MessageBox.Show("There are no details to update.");
                return;
            }

            List<DishDetailViewModel> updatedDishDetailViewModels = 
                (List<DishDetailViewModel>)dgwAddFood.DataSource;

            int selectedDishId = (int)dgwDishes.CurrentRow.Cells["Id"].Value;

            Dish selectedDish = UnitOfWork.Instance.DishRepository.GetById(selectedDishId);
            if (selectedDish == null)
            {
                MessageBox.Show("Selected dish not found.");
                return;
            }

            selectedDish.DishDetails.Clear(); 

            foreach (var updatedDetail in updatedDishDetailViewModels)
            {
                Food food = UnitOfWork.Instance.FoodRepository.GetAll().
                    FirstOrDefault(f => f.FoodName == updatedDetail.FoodName);

                if (food != null)
                {
                    DishDetail newDetail = new DishDetail
                    {
                        DishId = selectedDish.Id,
                        Dish = selectedDish,
                        FoodId = food.Id,
                        Food = food,
                        Gramme = updatedDetail.Gramme
                    };

                    selectedDish.DishDetails.Add(newDetail);
                }
            }

            
            selectedDish.CaloriesTaken = selectedDish.DishDetails.
                Sum(d => d.Food.CaloryPerGram * d.Gramme);

            
            UnitOfWork.Instance.DishRepository.Update(selectedDish);

            MessageBox.Show("Dish details have been updated.");

            
            List<Dish> updatedDishes = UnitOfWork.Instance.DishRepository.GetAll().
                Where(x => x.MealDate.Date == dtpMealDay.Value.Date).ToList();

            List<DishViewModel> updatedDishViewModels = GetDishViewModels(updatedDishes);

            dgwDishes.DataSource = updatedDishViewModels;
            dgwDishes.Columns["Id"].Visible = false;

            dgwAddFood.DataSource = null;
        }

        private void btnDeleteDish_Click(object sender, EventArgs e)
        {
            if (dgwDishes.SelectedRows.Count > 0)
            {
                int selectedDishId = (int)dgwDishes.CurrentRow.Cells["Id"].Value;

               

                if (selectedDishId != null)
                {
                    UnitOfWork.Instance.DishRepository.Remove(selectedDishId);

                    MessageBox.Show("The dish has been deleted.");

                    
                    List<Dish> updatedDishes = UnitOfWork.Instance.DishRepository.GetAll().
                        Where(x => x.MealDate.Date == dtpMealDay.Value.Date).ToList();

                    List<DishViewModel> updatedDishViewModels = GetDishViewModels(updatedDishes);

                    dgwDishes.DataSource = updatedDishViewModels;
                    dgwDishes.Columns["Id"].Visible = false;

                    dgwAddFood.DataSource = null;
                }
                else
                {
                    MessageBox.Show("Selected dish not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a dish to delete.");
            }
        }

       

        
    }
}
