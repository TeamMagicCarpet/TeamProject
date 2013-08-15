/// <reference path="class.js" />
/// <reference path="persister.js" />
/// <reference path="jquery-2.0.3.js" />
/// <reference path="ui.js" />

var controllers = (function () {

    var updateTimer = null;

    var rootUrl = "http://localhost:62248/api/";
    var Controller = Class.create({
        init: function () {
            this.persister = persisters.get(rootUrl);
        },
        loadUI: function (contentSelector, loginSelector) {
            if (this.persister.isUserLoggedIn()) {
                var welcomeHtml = ui.welcomeUI(this.persister.username());
                $(loginSelector).html(welcomeHtml);
            }
            else {
                this.loadLoginFormUI(loginSelector);
            }

            this.loadContentUI(contentSelector, loginSelector);

            this.attachUIEventHandlers(contentSelector, loginSelector);
        },
        loadLoginFormUI: function (selector) {
            var loginFormHtml = ui.loginForm()
            $(selector).html(loginFormHtml);
        },
        loadContentUI: function (contentSelector, loginSelector) {
            var self = this;
            $(contentSelector).empty();

            if (this.persister.isArticleSelected()) {
                self.persister.article.getArticle(function (data) {
                    var articleHtml = ui.articleUI(data);
                    $(contentSelector).html(articleHtml);
                });
            }
            else {
                if (this.persister.isUserLoggedIn()) {
                    $(contentSelector).html('<button id="new-article-button" class="btn" >New Article</button>');
                }
                self.persister.article.getAll(function (data) {
                    var mainContentHtml = ui.mainUI(data);
                    $(contentSelector).append(mainContentHtml);
                });
            }

            updateTimer = setInterval(function () {
                self.updateUI(contentSelector, loginSelector);
            }, 60000);
        },
        attachUIEventHandlers: function (contentSelector, loginSelector) {
            var wrapper = $(contentSelector);
            var self = this;

            wrapper.parent().on("click", "#login-button", function (e) {
                wrapper.parent().find("#login-popup").toggle();
                $(this).toggleClass("open-popup-btn");

                return false;
            });

            wrapper.parent().on("click", "#reg-button", function () {
                var regHtml = ui.registerUI();
                wrapper.html(regHtml);
                wrapper.parent().find("#login-popup").toggle();
                wrapper.parent().find("#login-button").toggleClass("open-popup-btn");
                return false;
            });

            wrapper.parent().on("click", "#submit-login-button", function (e) {
                var user = {
                    username: $("#tb-login-username").val(),
                    password: $("#tb-login-password").val()
                }

                self.persister.user.login(user, function () {
                    self.loadUI(contentSelector, loginSelector);
                });
                return false;
            });

            wrapper.parent().on("click", "#logout-button", function () {
                self.persister.user.logout(function () {
                    self.loadUI(contentSelector, loginSelector);
                });
                return false;
            });

            wrapper.on("click", "#submit-reg-button", function () {
                var pass = $(contentSelector).find("#tb-reg-password").val();
                var repPass = $(contentSelector).find("#tb-reg-reppassword").val();

                if (pass == repPass) {
                    var user = {
                        firstName: $(contentSelector).find("#tb-reg-firstname").val(),
                        lastName: $(contentSelector).find("#tb-reg-lastname").val(),
                        username: $(contentSelector).find("#tb-reg-username").val(),
                        password: pass,
                        email: $(contentSelector).find("#tb-reg-email").val()
                    }
                    self.persister.user.register(user, function () {
                        wrapper.find("#register-form").remove();
                        self.loadUI(contentSelector, loginSelector);
                    });
                }

                return false;
            });

            wrapper.on("click", "#new-article-button", function () {
                var newArticleHtml = ui.newArticleUI();
                wrapper.html(newArticleHtml);
                return false;
            });

            wrapper.on("click", "#submit-article", function () {
                //var formData = new FormData($('#file-select'));
                //$.ajax({
                //    url: 'http://localhost:62248/api/images/upload',
                //    type: 'POST',
                //    // Form data
                //    data: formData,
                //    //Options to tell JQuery not to process data or worry about content-type
                //    contentType: false
                //});


                var article = {
                    title: $(contentSelector).find("#tb-article-title").val(),
                    content: $(contentSelector).find("#area-article-content").val()
                };

                self.persister.article.create(article, function () {
                    self.loadUI(contentSelector, loginSelector);
                });
            });

            wrapper.on("click", ".article-title", function () {
                var title = $(this).text();
                var articleId = $(this).data("articleid");
                var articleData = {
                    Title: title,
                    ArticleId: articleId
                };
                self.persister.selectArticle(articleData);

                self.loadUI(contentSelector, loginSelector);
            });

            wrapper.on("click", "#back-button", function () {
                self.persister.deselectArticle();
                self.loadUI(contentSelector, loginSelector);
            });

            wrapper.on("click", ".answer-comment", function () {
                wrapper.find("#comment-answer-box").hide();
                $(this).siblings("#comment-answer-box").show();
            });

            wrapper.on("click", "#submit-answer", function () {
                var comment = {
                    content: $(contentSelector).find("#tb-comment-content").val(),
                    parentCommentId: $(this)
                        .parents("#comment-answer-box")
                        .siblings("button.btn answer-comment")
                        .data("answerid")
                };

                self.persister.comment.createAnswer(comment, function () {
                    wrapper.find("#comment-answer-box").hide();
                    self.loadUI(contentSelector, loginSelector);
                });
            });

            wrapper.on("click", "#submit-comment", function () {
                var comment = {
                    content: $(contentSelector).find("#tb-comment-content").val()
                };

                self.persister.comment.createComment(comment, function () {
                    wrapper.find("#comment-answer-box").hide();
                    self.loadUI(contentSelector, loginSelector);
                });
            });
        },
        updateUI: function (contentSelector, loginSelector) {
            this.loadUI(contentSelector, loginSelector);
        }
    });
    return {
        get: function () {
            return new Controller();
        }
    }
}());

$(function () {
    var controller = controllers.get();
    controller.loadUI("#content-main", "#login-reg");
});