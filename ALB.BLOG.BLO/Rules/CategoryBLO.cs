using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Rules
{
    public class CategoryBLO : ICategoryBLO
    {
        private readonly ICategoryDAO _categoryDAO;

        public CategoryBLO(ICategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        #region Public Methods
        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="categoryVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Create(CategoryVM categoryVM)
        {
            try
            {
                Category category;

                if (categoryVM == null)
                    throw new Exception("The object cannot be empty!");

                category = new Category(categoryVM.Name, categoryVM.Color);

                await _categoryDAO.Create(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Delete(int id)
        {
            try
            {
                var category = await _categoryDAO.GetCategoryByCategoryId(id);

                if (category == null)
                    throw new Exception("The category was not found!");

                await _categoryDAO.Delete(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<CategoryVM>> GetAllCategory()
        {
            try
            {
                var listOfCategories = await _categoryDAO.GetAllCategory();

                var listOfCategoriesVM = listOfCategories.Select(x => new CategoryVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color
                }).ToList();

                return listOfCategoriesVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CategoryVM> GetCategoryByCategoryId(int id)
        {
            try
            {
                var category = await _categoryDAO.GetCategoryByCategoryId(id);

                if (category == null)
                    throw new Exception("The category was not found!");

                var categoryVM = new CategoryVM()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Color = category.Color
                };

                return categoryVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Perform an update of a category.
        /// </summary>
        /// <param name="categoryVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Update(CategoryVM categoryVM)
        {
            try
            {
                Category category;

                var getCategory = await GetCategoryByCategoryId(categoryVM.Id);

                if (getCategory == null)
                    throw new Exception("The category was not found!");

                category = new Category(categoryVM.Id, categoryVM.Name, categoryVM.Color);

                Entity(categoryVM.Id, category);
                await _categoryDAO.Update(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Helper for updating a category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <exception cref="Exception"></exception>
        private void Entity(int id, Category category)
        {
            try
            {
                if (category == null)
                    throw new Exception("Fill the object, it cannot be empty!");

                _categoryDAO.Entity(id, category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}