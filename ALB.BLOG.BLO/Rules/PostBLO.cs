using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.ViewModels;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbUtilites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using X.PagedList;

namespace ALB.BLOG.BLO.Rules
{
    public class PostBLO : IPostBLO
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserBLO _applicationUserBLO;
        private readonly IGeneralBlogServices _generalBlogServices;
        private readonly IPostDAO _postDAO;
        private readonly IPostCategoryBLO _postCategoryBLO;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostBLO(UserManager<ApplicationUser> userManager, IApplicationUserBLO applicationUserBLO, IGeneralBlogServices generalBlogServices, IPostDAO postDAO, IPostCategoryBLO postCategoryBLO, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _applicationUserBLO = applicationUserBLO;
            _generalBlogServices = generalBlogServices;
            _postDAO = postDAO;
            _postCategoryBLO = postCategoryBLO;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Public Methods
        /// <summary>
        /// Create a post.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPostVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Create(string userName, CreatePostVM createPostVM)
        {
            try
            {
                Post post;
                PostCategory postCategory;
                string slug = null;
                string thumbnailUrl = null;

                var loggedInUser = await _applicationUserBLO.GetUserByNameIdentity(userName);

                if (createPostVM.Title != null)
                {
                    slug = createPostVM.Title!.Trim().Replace(" ", "-") + "-" + Guid.NewGuid();
                }

                if (createPostVM.Thumbnail != null)
                {
                    thumbnailUrl = _generalBlogServices.UploadImage(_webHostEnvironment, createPostVM.Thumbnail);
                }

                post = new Post(createPostVM.Title, createPostVM.ShortDescription, loggedInUser!.Id, DateTime.Now, createPostVM.Description, slug, thumbnailUrl);
                var postCreated = await _postDAO.Created(post);

                foreach (var categoryId in createPostVM.SelectedCategories)
                {
                    postCategory = new PostCategory(postCreated.Id, categoryId);
                    await _postCategoryBLO.Create(postCategory);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Delete(string userName, int id)
        {
            try
            {
                var post = await _postDAO.GetPostByIdPost(id);

                var loggedInUser = await _applicationUserBLO.GetUserByNameIdentity(userName);
                var loggedInUserRole = await _userManager.GetRolesAsync(loggedInUser!);

                if (loggedInUserRole[0] == WebsiteRoles.WebsiteAdmin || loggedInUser?.Id == post?.ApplicationUserId)
                {
                    await _postDAO.Delete(post);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all posts.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IPagedList<PostVM>> GetAllPost(int? page)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);

                var listOfPosts = await _postDAO.GetAllPost();

                var listOfPostsVM = listOfPosts.Select(x => new PostVM()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedDate = x.CreatedDate,
                    ThumbnailUrl = x.ThumbnailUrl,
                    AuthorName = x.ApplicationUser!.FirstName + " " + x.ApplicationUser.LastName
                }).ToList();

                var posts = listOfPostsVM.OrderByDescending(x => x.CreatedDate).ToList();

                return await posts.ToPagedListAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all posts with a filter.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchFilter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IPagedList<Post>> GetAllPostSearch(int? page, string? searchFilter = null, int[]? categories = null)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);

                var listOfPosts = await _postDAO.GetAllPostSearch(searchFilter, categories);

                return await listOfPosts.ToPagedListAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get post by ID and validate the user by name has access.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CreatePostVM?> GetEditPost(string userName, int id)
        {
            try
            {
                var postVM = await GetPostByIdPost(id);

                var loggedInUser = await _applicationUserBLO.GetUserByNameIdentity(userName);
                var loggedInUserRole = await _userManager.GetRolesAsync(loggedInUser!);

                if (loggedInUserRole[0] != WebsiteRoles.WebsiteAdmin && loggedInUser!.Id != postVM.ApplicationUserId)
                {
                    throw new Exception("You are not authorized");
                }

                return postVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Take the list of posts, complete or by user, and add pagination.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IPagedList<PostVM>> GetIndexPost(string userName, int? page)
        {
            try
            {
                IPagedList<PostVM> listOfPosts;

                var loggedInUser = await _applicationUserBLO.GetUserByNameIdentity(userName);
                var loggedInUserRole = await _userManager.GetRolesAsync(loggedInUser!);

                if (loggedInUserRole[0] == WebsiteRoles.WebsiteAdmin)
                {
                    listOfPosts = await GetAllPost(page);
                }
                else
                {
                    listOfPosts = await GetPostsByIdUser(page, loggedInUser!.Id);
                }

                return listOfPosts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a post by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CreatePostVM?> GetPostByIdPost(int id)
        {
            try
            {
                var post = await _postDAO.GetPostByIdPost(id);

                if (post == null)
                    return new CreatePostVM();

                var createPostVM = new CreatePostVM()
                {
                    Id = post.Id,
                    Title = post.Title,
                    ShortDescription = post.ShortDescription,
                    Description = post.Description,
                    ThumbnailUrl = post.ThumbnailUrl,
                    SelectedCategories = await _postCategoryBLO.GetCategoriesByIdPost(id)
                };

                return createPostVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a post by User ID.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IPagedList<PostVM>> GetPostsByIdUser(int? page, string idUser)
        {
            try
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);

                var listOfPosts = await _postDAO.GetPostsByIdUser(idUser);

                var listOfPostsVM = listOfPosts.Select(x => new PostVM()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedDate = x.CreatedDate,
                    ThumbnailUrl = x.ThumbnailUrl,
                    AuthorName = x.ApplicationUser!.FirstName + " " + x.ApplicationUser.LastName
                }).ToList();

                var posts = listOfPostsVM.OrderByDescending(x => x.CreatedDate).ToList();

                return await posts.ToPagedListAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get a post by slug.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<BlogPostVM?> GetPostBySlug(string slug)
        {
            try
            {
                if (slug == "")
                    return new BlogPostVM();

                var post = await _postDAO.GetPostBySlug(slug);

                if (post == null)
                    return new BlogPostVM();

                var blogPostVM = new BlogPostVM()
                {
                    Id = post.Id,
                    Title = post.Title,
                    AuthorName = post.ApplicationUser!.FirstName + " " + post.ApplicationUser.LastName,
                    CreatedDate = post.CreatedDate,
                    ThumbnailUrl = post.ThumbnailUrl ?? "/blog/assets/img/home-bg.jpg",
                    Description = post.Description,
                    ShortDescription = post.ShortDescription,
                    PostCategories = post.PostCategories
                };

                return blogPostVM;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Update a post.
        /// </summary>
        /// <param name="createPostVM"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> Update(CreatePostVM createPostVM)
        {
            try
            {
                Post post;
                PostCategory postCategory;
                string thumbnailUrl = null;

                var getPost = await _postDAO.GetPostByIdPost(createPostVM.Id);

                if (getPost == null)
                    return false;

                if (createPostVM.Thumbnail != null)
                {
                    thumbnailUrl = _generalBlogServices.UploadImage(_webHostEnvironment, createPostVM.Thumbnail);
                }
                else
                {
                    thumbnailUrl = getPost.ThumbnailUrl;
                }

                post = new Post(createPostVM.Id, createPostVM.Title, createPostVM.ShortDescription, getPost.ApplicationUserId, getPost.CreatedDate, createPostVM.Description, getPost.Slug, thumbnailUrl);

                Entity(createPostVM.Id, post);
                await _postDAO.Update(post);

                await _postCategoryBLO.Delete(createPostVM.Id);

                foreach (var categoryId in createPostVM.SelectedCategories)
                {
                    postCategory = new PostCategory(createPostVM.Id, categoryId);
                    await _postCategoryBLO.Create(postCategory);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Helper for updating a post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <exception cref="Exception"></exception>
        private void Entity(int id, Post post)
        {
            try
            {
                if (post == null)
                    throw new Exception("Fill the object, it cannot be empty!");

                _postDAO.Entity(id, post);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}