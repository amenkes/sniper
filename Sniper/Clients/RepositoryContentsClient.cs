﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Sniper.Http;
using Sniper.Request;
using Sniper.Response;
using Sniper.ToBeRemoved;

namespace Sniper
{
    /// <summary>
    /// Client for accessing contents of files within a repository as base64 encoded content.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/contents/">Repository Contents API documentation</a> for more information.
    /// </remarks>
    public class RepositoryContentsClient : ApiClient //, IRepositoryContentsClient
    {
        /// <summary>
        /// Create an instance of the RepositoryContentsClient
        /// </summary>
        /// <param name="apiConnection">The underlying connection to use</param>
        public RepositoryContentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            
            var url = ApiUrls.RepositoryContent(owner, name, path);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The content path</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(long repositoryId, string path)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);

            var url = ApiUrls.RepositoryContent(repositoryId, path);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);

            var url = ApiUrls.RepositoryContent(owner, name, string.Empty);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContents(long repositoryId)
        {
            var url = ApiUrls.RepositoryContent(repositoryId, string.Empty);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The content path</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository�s default branch (usually master)</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(OldGitHubToBeRemoved.Reference, reference);

            var url = ApiUrls.RepositoryContent(owner, name, path, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of a file or directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The content path</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(long repositoryId, string path, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(OldGitHubToBeRemoved.Reference, reference);

            var url = ApiUrls.RepositoryContent(repositoryId, path, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository�s default branch (usually master)</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);
            Ensure.ArgumentNotNull(OldGitHubToBeRemoved.Reference, reference);

            var url = ApiUrls.RepositoryContent(owner, name, string.Empty, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Returns the contents of the root directory in a repository.
        /// </summary>
        /// <remarks>
        /// If given a path to a single file, this method returns a collection containing only that file.
        /// See the <a href="https://developer.github.com/v3/repos/contents/#get-contents">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the commit/branch/tag. Default: the repository’s default branch (usually master)</param>
        public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNull(OldGitHubToBeRemoved.Reference, reference);

            var url = ApiUrls.RepositoryContent(repositoryId, string.Empty, reference);

            return ApiConnection.GetAll<RepositoryContent>(url);
        }

        /// <summary>
        /// Gets the preferred README for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);

            var endpoint = ApiUrls.RepositoryReadme(owner, name);
            var readmeInfo = await ApiConnection.Get<ReadmeResponse>(endpoint, null).ConfigureAwait(false);

            return new Readme(readmeInfo, ApiConnection);
        }

        /// <summary>
        /// Gets the preferred README for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<Readme> GetReadme(long repositoryId)
        {
            var endpoint = ApiUrls.RepositoryReadme(repositoryId);
            var readmeInfo = await ApiConnection.Get<ReadmeResponse>(endpoint, null).ConfigureAwait(false);

            return new Readme(readmeInfo, ApiConnection);
        }

        /// <summary>
        /// Gets the preferred README's HTML for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);

            return ApiConnection.GetHtml(ApiUrls.RepositoryReadme(owner, name), null);
        }

        /// <summary>
        /// Gets the preferred README's HTML for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/contents/#get-the-readme">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<string> GetReadmeHtml(long repositoryId)
        {
            return ApiConnection.GetHtml(ApiUrls.RepositoryReadme(repositoryId), null);
        }

       

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        public Task<RepositoryContentChangeSet> CreateFile(string owner, string name, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(HttpKeys.RequestParameters.Request, request);

            var createUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(createUrl, request);
        }

        /// <summary>
        /// Creates a commit that creates a new file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to create</param>
        public Task<RepositoryContentChangeSet> CreateFile(long repositoryId, string path, CreateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(HttpKeys.RequestParameters.Request, request);

            var createUrl = ApiUrls.RepositoryContent(repositoryId, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(createUrl, request);
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        public Task<RepositoryContentChangeSet> UpdateFile(string owner, string name, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(HttpKeys.RequestParameters.Request, request);

            var updateUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(updateUrl, request);
        }

        /// <summary>
        /// Creates a commit that updates the contents of a file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to update</param>
        public Task<RepositoryContentChangeSet> UpdateFile(long repositoryId, string path, UpdateFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(HttpKeys.RequestParameters.Request, request);

            var updateUrl = ApiUrls.RepositoryContent(repositoryId, path);
            return ApiConnection.Put<RepositoryContentChangeSet>(updateUrl, request);
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        public Task DeleteFile(string owner, string name, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Owner, owner);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Name, name);
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(HttpKeys.RequestParameters.Request, request);

            var deleteUrl = ApiUrls.RepositoryContent(owner, name, path);
            return ApiConnection.Delete(deleteUrl, request);
        }

        /// <summary>
        /// Creates a commit that deletes a file in a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="path">The path to the file</param>
        /// <param name="request">Information about the file to delete</param>
        public Task DeleteFile(long repositoryId, string path, DeleteFileRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(OldGitHubToBeRemoved.Path, path);
            Ensure.ArgumentNotNull(HttpKeys.RequestParameters.Request, request);

            var deleteUrl = ApiUrls.RepositoryContent(repositoryId, path);
            return ApiConnection.Delete(deleteUrl, request);
        }
    }
}