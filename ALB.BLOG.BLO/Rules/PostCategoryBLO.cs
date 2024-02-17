using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;

namespace ALB.BLOG.BLO.Rules
{
    public class PostCategoryBLO : IPostCategoryBLO
    {
        private readonly IPostCategoryDAO _postCategoryDAO;

        public PostCategoryBLO(IPostCategoryDAO postCategoryDAO)
        {
            _postCategoryDAO = postCategoryDAO;
        }

        /// <summary>
        /// Create a post category.
        /// </summary>
        /// <param name="postCategory"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Create(PostCategory postCategory)
        {
            try
            {
                if (postCategory == null)
                    throw new Exception("The object cannot be empty!");

                await _postCategoryDAO.Create(postCategory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a post category.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Delete(int? postId = null, int? categoryId = null)
        {
            try
            {
                if (postId != null && categoryId == null)
                {
                    List<PostCategory> listPostCategories = (List<PostCategory>)await _postCategoryDAO.GetPostCategoryByIdPost((int)postId);

                    foreach (var post in listPostCategories)
                    {
                        await _postCategoryDAO.Delete(post);
                    }
                }
                else
                {
                    PostCategory postCategory = await _postCategoryDAO.GetPostCategoryByIdPostAndCategory((int)postId, (int)categoryId);

                    if (postCategory == null)
                        throw new Exception("Relationship not found!");

                    await _postCategoryDAO.Delete(postCategory);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all posts categories.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<PostCategoryVM>> GetAllPostCategory()
        {
            try
            {
                var postCategory = await _postCategoryDAO.GetAllPostCategory();

                var listOfPostCategoryVM = postCategory.Select(x => new PostCategoryVM()
                {
                    PostId = x.PostId,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    CategoryColor = x.Category.Color
                }).ToList();

                return listOfPostCategoryVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get posts categories by id post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<PostCategoryVM>> GetPostCategoryByIdPost(int id)
        {
            try
            {
                var postCategory = await _postCategoryDAO.GetPostCategoryByIdPost(id);

                if (postCategory == null)
                    throw new Exception("No Post Category was found with this Id!");

                var listOfPostCategoryVM = postCategory.Select(x => new PostCategoryVM()
                {
                    PostId = x.PostId,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    CategoryColor = x.Category.Color
                }).ToList();

                return listOfPostCategoryVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Categories by ID Post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<int>> GetCategoriesByIdPost(int id)
        {
            try
            {
                var postCategory = await _postCategoryDAO.GetPostCategoryByIdPost(id);

                if (postCategory == null)
                    throw new Exception("No Post Category was found with this Id!");

                var listOfPostCategoryIds = postCategory.Select(x => x.CategoryId).ToList();

                return listOfPostCategoryIds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get posts categories by id post and category.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PostCategoryVM> GetPostCategoryByIdPostAndCategory(int postId, int categoryId)
        {
            try
            {
                var postCategory = await _postCategoryDAO.GetPostCategoryByIdPostAndCategory(postId, categoryId);

                if (postCategory == null)
                    throw new Exception("No Post Category was found with this Id!");

                var postCategoryVM = new PostCategoryVM()
                {
                    PostId = postCategory.PostId,
                    CategoryId = postCategory.CategoryId,
                    CategoryName = postCategory.Category.Name,
                    CategoryColor = postCategory.Category.Color
                };

                return postCategoryVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}