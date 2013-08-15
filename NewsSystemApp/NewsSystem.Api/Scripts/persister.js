/// <reference path="http-requester.js" />
/// <reference path="class.js" />
/// <reference path="CryptoJS-SHA1.js" />

var persisters = (function () {
    var username = localStorage.getItem("username");
    var userId = localStorage.getItem("userId");
    var sessionKey = localStorage.getItem("sessionKey");
    var articleTitle = localStorage.getItem("articleTitle");
    var articleId = localStorage.getItem("articleId");

    function saveUserData(userData) {
        localStorage.setItem("username", userData.UserName);
        localStorage.setItem("userId", userData.UserId);
        localStorage.setItem("sessionKey", userData.SessionKey);
        username = userData.UserName;
        userId = userData.UserId;
        sessionKey = userData.SessionKey;
    }
    function clearUserData() {
        localStorage.removeItem("username");
        localStorage.removeItem("userId");
        localStorage.removeItem("sessionKey");
        username = null;
        userId = null;
        sessionKey = null;
    }

    function saveArticleData(articleData) {
        localStorage.setItem("articleTitle", articleData.Title);
        localStorage.setItem("articleId", articleData.ArticleId);
        articleTitle = articleData.Title;
        articleId = articleData.ArticleId;
    }
    function clearArticleData() {
        localStorage.removeItem("articleTitle");
        localStorage.removeItem("articleId");
        articleTitle = null;
        articleId = null;
    }

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.user = new UserPersister(this.rootUrl);
            this.article = new ArticlePersister(this.rootUrl);
            this.comment = new CommentPersister(this.rootUrl);
            this.vote = new VotePersister(this.rootUrl);
            this.image = new ImagePersister(this.rootUrl);
        },
        isUserLoggedIn: function () {
            var isLoggedIn = username != null && userId != null && sessionKey != null;
            return isLoggedIn;
        },
        isArticleSelected: function () {
            var isSelected = articleTitle != null && articleId != null;
            return isSelected;
        },
        username: function () {
            return username;
        },
        selectArticle: function (articleData) {
            saveArticleData(articleData);
        },
        deselectArticle: function () {
            clearArticleData();
        }
    });
    var UserPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl + "users/";
        },
        login: function (user, success, error) {
            var url = this.rootUrl + "login";
            var userData = {
                username: user.username,
                password: CryptoJS.SHA1(user.password).toString()
            };

            httpRequester.postJSON(url, userData,
				function (data) {
				    saveUserData(data);
				    success(data);
				}, error);
        },
        register: function (user, success, error) {
            var url = this.rootUrl + "register";
            var userData = {
                firstName: user.firstName,
                lastName: user.lastName,
                username: user.username,
                password: CryptoJS.SHA1(user.password).toString(),
                email: user.email
            };
            httpRequester.postJSON(url, userData,
				function (data) {
				    saveUserData(data);
				    success(data);
				}, error);
        },
        logout: function (success, error) {
            // TODO: Check logout
            var url = this.rootUrl + "logout/" + sessionKey;
            httpRequester.getJSON(url, function (data) {
                clearUserData();
                success(data);
            }, error)
        }
    });

    var ArticlePersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "articles/";
        },
        create: function (article, success, error) {
            var url = this.rootUrl + "create";
            var articleData = {
                title: article.title,
                content: article.content,
                authorId: userId,
                image1: article.images[0]
            };

            httpRequester.postJSON(url, articleData,
                function (data) {

                    success(data);
                }, error);
        },
        getArticle: function (success, error) {
            var url = this.rootUrl + "getarticle/" + articleId;

            httpRequester.getJSON(url, success, error);
        },
        getAll: function (success, error) {
            var url = this.rootUrl + "getall";

            httpRequester.getJSON(url, success, error);
        }
    });

    var CommentPersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "comments/";
        },
        createAnswer: function (comment, success, error) {
            var url = this.rootUrl + "create";
            var commentData = {
                content: comment.content,
                articleId: articleId,
                authorId: userId,
                isSubComment: true,
                ParrentCommentId: comment.parentCommentId.toString()
            };

            httpRequester.postJSON(url, commentData, success, error);
        },
        createComment: function (comment, success, error) {
            var url = this.rootUrl + "create";
            var commentData = {
                content: comment.content,
                articleId: articleId,
                authorId: userId
            };

            httpRequester.postJSON(url, commentData, success, error);
        }
    });

    var VotePersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "votes/";
        },
        create: function (vote, success, error) {
            var url = this.rootUrl + "create";
            var voteData = {
                value: vote.value,
                userId: userId,
                articleId: articleId
            };

            httpRequester.postJSON(url, voteData, success, error);
        }
    });

    var ImagePersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "images/";
        },
        upload: function (formData, success, error) {
            var url = this.rootUrl + "upload";
            var imageData = {
                formData: formData
            };

            httpRequester.postJSON(url, imageData, success, error);
        }
    });

    return {
        get: function (url) {
            return new MainPersister(url);
        }
    };
}());