/// <reference path="http-requester.js" />
/// <reference path="class.js" />
/// <reference path="CryptoJS-SHA1.js" />

var persisters = (function () {
    var username = localStorage.getItem("username");
    var sessionKey = localStorage.getItem("sessionKey");
    var articleTitle = localStorage.getItem("articleTitle");
    var articleId = localStorage.getItem("articleId");

    function saveUserData(userData) {
        localStorage.setItem("username", userData.UserName);
        localStorage.setItem("sessionKey", userData.SessionKey);
        username = userData.UserName;
        sessionKey = userData.SessionKey;
    }
    function clearUserData() {
        localStorage.removeItem("username");
        localStorage.removeItem("sessionKey");
        username = null;
        sessionKey = null;
    }

    function saveArticleData(articleData) {
        localStorage.setItem("articleTitle", articleData.articleTitle);
        localStorage.setItem("articleId", articleData.articleId);
        articleTitle = articleData.articleTitle;
        articleId = articleData.articleId;
    }
    function clearArticleData() {
        localStorage.removeItem("articleTitle");
        localStorage.removeItem("articleId");
        articleTitle = "";
        articleId = "";
    }

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.user = new UserPersister(this.rootUrl);
            this.article = new ArticlePersister(this.rootUrl);
            this.comment = new CommentPersister(this.rootUrl);
            //        this.game = new GamePersister(this.rootUrl);
            //        this.battle = new BattlePersister(this.rootUrl);
            //        this.message = new MessagesPersister(this.rootUrl);
        },
        isUserLoggedIn: function () {
            var isLoggedIn = username != null && sessionKey != null;
            return isLoggedIn;
        },
        isArticleSelected: function () {
            var isSelected = articleTitle != null && articleId != null;
            return isSelected;
        },
        username: function () {
            return username;
        }
    });
    var UserPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl + "users/";
        },
        login: function (user, success, error) {
            // TODO: Check login
            var url = this.rootUrl + "login";
            var userData = {
                username: user.username,
                // TODO: authCode?
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
        },
        //    scores: function (success, error) {
        //        var url = this.rootUrl + "scores/" + sessionKey;
        //        httpRequester.getJSON(url, success, error);
        //    }
    });

    var ArticlePersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "article/";
        },
        //TODO: Article Persister
        //create: function (game, success, error) {
        //    var gameData = {
        //        title: game.title,
        //    };
        //    if (game.password) {
        //        gameData.password = CryptoJS.SHA1(game.password).toString();
        //    }
        //    var url = this.rootUrl + "create/" + sessionKey;
        //    httpRequester.postJSON(url, gameData, success, error);
        //},
        //join: function (game, success, error) {
        //    var gameData = {
        //        id: game.id,
        //    };
        //    if (game.password) {
        //        gameData.password = CryptoJS.SHA1(game.password).toString();
        //    }
        //    var url = this.rootUrl + "join/" + sessionKey;
        //    httpRequester.postJSON(url, gameData, success, error);
        //},
        //start: function (gameId, success, error) {
        //    var url = this.rootUrl + gameId + "/start/" + sessionKey;
        //    httpRequester.getJSON(url, success, error)
        //},
        //myActive: function (success, error) {
        //    var url = this.rootUrl + "my-active/" + sessionKey;
        //    httpRequester.getJSON(url, success, error);
        //},
        //open: function (success, error) {
        //    var url = this.rootUrl + "open/" + sessionKey;
        //    httpRequester.getJSON(url, success, error);
        //},
        //field: function (gameId, success, error) {
        //    var url = this.rootUrl + gameId + "/field/" + sessionKey;
        //    httpRequester.getJSON(url, success, error);
        //}
    });

    var CommentPersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "comment/";
        },
        // TODO: CommentPersister
        //move: function (data, success, error) {
        //    var gameData = {
        //        unitId: data.unitId,
        //        position: data.position
        //    };
        //    var url = this.rootUrl + data.gameId + "/move/" + sessionKey;
        //    httpRequester.postJSON(url, gameData, success, error);
        //},
        //attack: function (data, success, error) {
        //    var gameData = {
        //        unitId: data.unitId,
        //        position: data.position
        //    };
        //    var url = this.rootUrl + data.gameId + "/attack/" + sessionKey;
        //    httpRequester.postJSON(url, gameData, success, error);
        //},
        //defend: function (data, success, error) {
        //    var gameData = data.unitId;
        //    var url = this.rootUrl + data.gameId + "/defend/" + sessionKey;
        //    httpRequester.postJSON(url, gameData, success, error);
        //}
    });
    //var MessagesPersister = Class.create({
    //    init: function (url) {
    //        this.rootUrl = url + "messages/";
    //    },
    //    unread: function (success, error) {
    //        var url = this.rootUrl + "unread/" + sessionKey;
    //        httpRequester.getJSON(url, success, error);
    //    },
    //    all: function (success, error) {
    //        var url = this.rootUrl + "all/" + sessionKey;
    //        httpRequester.getJSON(url, success, error);
    //    }
    //});
    return {
        get: function (url) {
            return new MainPersister(url);
        }
    };
}());