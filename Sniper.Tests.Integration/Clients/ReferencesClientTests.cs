﻿using System;
using System.Threading.Tasks;
using Sniper.Tests.Integration.Helpers;
using Xunit;

namespace Sniper.Tests.Integration.Clients
{
    public class ReferencesClientTests : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly IReferencesClient _fixture;
        private readonly RepositoryContext _context;

        public ReferencesClientTests()
        {
            _github = Helper.GetAuthenticatedClient();

            _fixture = _github.Git.Reference;

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanGetAReference()
        {
            var @ref = await _fixture.Get("Sniper", "Sniper.net", "heads/master");

            // validate the top-level properties
            Assert.Equal("refs/heads/master", @ref.Ref);
            Assert.Equal("https://api.github.com/repos/Sniper/Sniper.net/git/refs/heads/master", @ref.Url);

            // validate the git reference
            Assert.Equal(TaggedType.Commit, @ref.Object.Type);
            Assert.False(string.IsNullOrWhiteSpace(@ref.Object.Sha));
        }

        [IntegrationTest]
        public async Task CanGetAReferenceWithRepositoryId()
        {
            var @ref = await _fixture.Get(7528679, "heads/master");

            // validate the top-level properties
            Assert.Equal("refs/heads/master", @ref.Ref);
            Assert.Equal("https://api.github.com/repos/Sniper/Sniper.net/git/refs/heads/master", @ref.Url);

            // validate the git reference
            Assert.Equal(TaggedType.Commit, @ref.Object.Type);
            Assert.False(string.IsNullOrWhiteSpace(@ref.Object.Sha));
        }

        [IntegrationTest]
        public async Task WhenReferenceDoesNotExistAnExceptionIsThrown()
        {
            await Assert.ThrowsAsync<NotFoundException>(
                () => _fixture.Get("Sniper", "Sniper.net", "heads/foofooblahblah"));
        }

        [IntegrationTest(Skip = "This is paging for a long long time")]
        public async Task CanGetListOfReferences()
        {
            var list = await _fixture.GetAll("Sniper", "Sniper.net");
            Assert.NotEmpty(list);
        }

        [IntegrationTest(Skip = "This is paging for a long long time")]
        public async Task CanGetListOfReferencesWithRepositoryId()
        {
            var list = await _fixture.GetAll(7528679);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfReferencesInNamespace()
        {
            var list = await _fixture.GetAllForSubNamespace("Sniper", "Sniper.net", "heads");
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfReferencesInNamespaceWithRepositoryId()
        {
            var list = await _fixture.GetAllForSubNamespace(7528679, "heads");
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetErrorForInvalidNamespace()
        {
            var owner = "Sniper";
            var repo = "Sniper.net";
            var subNamespace = "666";

            var result = await Assert.ThrowsAsync<NotFoundException>(
                async () => { await _fixture.GetAllForSubNamespace(owner, repo, subNamespace); });
            Assert.Equal(string.Format("{0} was not found.", ApiUrls.Reference(owner, repo, subNamespace)), result.Message);
        }

        [IntegrationTest]
        public async Task CanCreateAReference()
        {
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

            var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

            var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

            var newReference = new NewReference("heads/develop", commitResult.Sha);
            var result = await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newReference);

            Assert.Equal(commitResult.Sha, result.Object.Sha);
        }

        [IntegrationTest]
        public async Task CanCreateAReferenceWithRepositoryId()
        {
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

            var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

            var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

            var newReference = new NewReference("heads/develop", commitResult.Sha);
            var result = await _fixture.Create(_context.Repository.Id, newReference);

            Assert.Equal(commitResult.Sha, result.Object.Sha);
        }

        [IntegrationTest]
        public async Task CanUpdateAReference()
        {
            var firstBlob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var firstBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, firstBlob);
            var secondBlob = new NewBlob
            {
                Content = "This is a test!",
                Encoding = EncodingType.Utf8
            };
            var secondBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, secondBlob);

            var firstTree = new NewTree();
            firstTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = firstBlobResult.Sha
            });

            var firstTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, firstTree);
            var firstCommit = new NewCommit("This is a new commit", firstTreeResult.Sha);
            var firstCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, firstCommit);

            var newReference = new NewReference("heads/develop", firstCommitResult.Sha);
            await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newReference);

            var secondTree = new NewTree();
            secondTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = secondBlobResult.Sha
            });

            var secondTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, secondTree);

            var secondCommit = new NewCommit("This is a new commit", secondTreeResult.Sha, firstCommitResult.Sha);
            var secondCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, secondCommit);

            var referenceUpdate = new ReferenceUpdate(secondCommitResult.Sha);

            var result = await _fixture.Update(_context.RepositoryOwner, _context.RepositoryName, "heads/develop", referenceUpdate);

            Assert.Equal(secondCommitResult.Sha, result.Object.Sha);
        }

        [IntegrationTest]
        public async Task CanUpdateAReferenceWithRepositoryId()
        {
            var firstBlob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var firstBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, firstBlob);
            var secondBlob = new NewBlob
            {
                Content = "This is a test!",
                Encoding = EncodingType.Utf8
            };
            var secondBlobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, secondBlob);

            var firstTree = new NewTree();
            firstTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = firstBlobResult.Sha
            });

            var firstTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, firstTree);
            var firstCommit = new NewCommit("This is a new commit", firstTreeResult.Sha);
            var firstCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, firstCommit);

            var newReference = new NewReference("heads/develop", firstCommitResult.Sha);
            await _fixture.Create(_context.Repository.Id, newReference);

            var secondTree = new NewTree();
            secondTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = secondBlobResult.Sha
            });

            var secondTreeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, secondTree);

            var secondCommit = new NewCommit("This is a new commit", secondTreeResult.Sha, firstCommitResult.Sha);
            var secondCommitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, secondCommit);

            var referenceUpdate = new ReferenceUpdate(secondCommitResult.Sha);

            var result = await _fixture.Update(_context.Repository.Id, "heads/develop", referenceUpdate);

            Assert.Equal(secondCommitResult.Sha, result.Object.Sha);
        }

        [IntegrationTest]
        public async Task CanDeleteAReference()
        {
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

            var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

            var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

            var newReference = new NewReference("heads/develop", commitResult.Sha);

            await _fixture.Create(_context.RepositoryOwner, _context.RepositoryName, newReference);
            await _fixture.Delete(_context.RepositoryOwner, _context.RepositoryName, "heads/develop");

            var all = await _fixture.GetAll(_context.RepositoryOwner, _context.RepositoryName);

            Assert.Empty(all.Where(r => r.Ref == "heads/develop"));
        }

        [IntegrationTest]
        public async Task CanDeleteAReferenceWithRepositoryId()
        {
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await _github.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Mode = FileMode.File,
                Type = TreeType.Blob,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await _github.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

            var newCommit = new NewCommit("This is a new commit", treeResult.Sha);

            var commitResult = await _github.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);

            var newReference = new NewReference("heads/develop", commitResult.Sha);

            await _fixture.Create(_context.Repository.Id, newReference);
            await _fixture.Delete(_context.Repository.Id, "heads/develop");

            var all = await _fixture.GetAll(_context.Repository.Id);

            Assert.Empty(all.Where(r => r.Ref == "heads/develop"));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
