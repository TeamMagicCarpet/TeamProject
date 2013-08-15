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

            //TODO: Make Event UI Handlers

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
                }/*, function (err) {
                    wrapper.find("#error-messages").text(err.responseJSON.Message);
                }*/);
                return false;
            });

            wrapper.parent().on("click", "#logout-button", function () {
                self.persister.user.logout(function () {
                    self.loadUI(contentSelector, loginSelector);
                }/*, function (err) {
                    wrapper.find("#error-messages").text(err.responseJSON.Message);
                }*/);
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
                    }/*, function (err) {
                        wrapper.find("#error-messages").text(err.responseJSON.Message);
                    }*/);
                }

                return false;
            });

            wrapper.on("click", "#new-article-button", function () {
                var newArticleHtml = ui.newArticleUI();
                wrapper.html(newArticleHtml);
                return false;
            });

            wrapper.on("click", "#submit-article", function () {
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

            //        wrapper.on("click", "#btn-show-login", function () {
            //            wrapper.find(".button.selected").removeClass("selected");
            //            $(this).addClass("selected");
            //            wrapper.find("#login-form").show();
            //            wrapper.find("#register-form").hide();
            //        });
            //        wrapper.on("click", "#btn-show-register", function () {
            //            wrapper.find(".button.selected").removeClass("selected");
            //            $(this).addClass("selected");
            //            wrapper.find("#register-form").show();
            //            wrapper.find("#login-form").hide();
            //        });

            //        wrapper.on("click", "#btn-login", function () {
            //            var user = {
            //                username: $(selector + " #tb-login-username").val(),
            //                password: $(selector + " #tb-login-password").val()
            //            }

            //            self.persister.user.login(user, function () {
            //                self.loadGameUI(selector);
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });
            //            return false;
            //        });
            //        wrapper.on("click", "#btn-register", function () {
            //            var user = {
            //                username: $(selector).find("#tb-register-username").val(),
            //                nickname: $(selector).find("#tb-register-nickname").val(),
            //                password: $(selector + " #tb-register-password").val()
            //            }
            //            self.persister.user.register(user, function () {
            //                self.loadGameUI(selector);
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });
            //            return false;
            //        });
            //        wrapper.on("click", "#btn-logout", function () {
            //            self.persister.user.logout(function () {
            //                self.loadLoginFormUI(selector);
            //                clearInterval(updateTimer);
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });
            //        });

            //        wrapper.on("click", "#open-games-container a", function () {
            //            $("#game-join-inputs").remove();
            //            var html =
            //				'<div id="game-join-inputs">' +
            //					'Password: <input type="text" id="tb-game-pass"/><br/>' +
            //					'<button id="btn-join-game">join</button>' +
            //				'</div>';
            //            $(this).after(html);
            //        });
            //        wrapper.on("click", "#btn-join-game", function () {
            //            var game = {
            //                number: $("#tb-game-number").val(),
            //                id: $(this).parents("li").first().data("game-id")
            //            };

            //            var password = $("#tb-game-pass").val();

            //            if (password) {
            //                game.password = password;
            //            }
            //            self.persister.game.join(game, function () {

            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });
            //        });
            //        wrapper.on("click", "#btn-create-game", function () {
            //            var game = {
            //                title: $("#tb-create-title").val(),
            //            }
            //            var password = $("#tb-create-pass").val();
            //            if (password) {
            //                game.password = password;
            //            }
            //            self.persister.game.create(game, function () {
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });
            //        });

            //        wrapper.on("click", "#active-games-container li.game-status-full a.btn-active-game", function () {
            //            var gameCreator = $(this).parent().data("creator");
            //            var myNickname = self.persister.nickname();
            //            if (gameCreator == myNickname) {
            //                $("#btn-game-start").remove();
            //                var html =
            //					'<a href="#" id="btn-game-start">' +
            //						'Start' +
            //					'</a>';
            //                $(this).parent().append(html);
            //            }
            //        });

            //        wrapper.on("click", "#btn-game-start", function () {
            //            var parent = $(this).parent();

            //            var gameId = parent.data("game-id");
            //            self.persister.game.start(gameId, function () {
            //                wrapper.find("#game-holder").html("started");
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });

            //            return false;
            //        });

            //        wrapper.on("click", ".active-games .game-status-in-progress", function () {
            //            self.loadGame(selector, $(this).data("game-id"));
            //        });

            //        wrapper.on('click', '#game-field .game-unit.in-turn', function () {
            //            $('.unit-defend-action').css('display', 'none');
            //            $(this).children('.unit-defend-action').css('display', 'inline-block');
            //            $('.selected-unit').removeClass('selected-unit');
            //            $(this).addClass('selected-unit');
            //        });

            //        wrapper.on('click', '#game-field .empty', function () {
            //            var selected = $('.selected-unit');
            //            if (selected.length > 0) {
            //                var gameId = selected.parents('#game-state-container').first().data('game-id');
            //                var positionStr = $(this).data('position');
            //                var positionArr = positionStr.split(' ');
            //                var data = {
            //                    gameId: gameId,
            //                    unitId: selected.first().data('unit-id'),
            //                    position: { x: positionArr[0], y: positionArr[1] }
            //                };
            //                self.persister.battle.move(data, function () {
            //                }, function (err) {
            //                    wrapper.find("#error-messages").text(err.responseJSON.Message);
            //                });
            //            }
            //        });

            //        wrapper.on('click', '#game-field .game-unit:not(.in-turn)', function () {
            //            var selected = $('.selected-unit');
            //            if (selected.length > 0) {
            //                var gameId = selected.parents('#game-state-container').first().data('game-id');
            //                var positionStr = $(this).data('position');
            //                var positionArr = positionStr.split(' ');
            //                var data = {
            //                    gameId: gameId,
            //                    unitId: selected.first().data('unit-id'),
            //                    position: { x: positionArr[0], y: positionArr[1] }
            //                };
            //                self.persister.battle.attack(data, function () {
            //                }, function (err) {
            //                    wrapper.find("#error-messages").text(err.responseJSON.Message);
            //                });
            //            }
            //        });

            //        wrapper.on('click', '#game-field .game-unit .unit-defend-action', function () {
            //            //debugger;
            //            var data = {
            //                unitId: $(this).parents('.game-unit').first().data('unit-id'),
            //                gameId: $('.selected-unit').parents('#game-state-container').first().data('game-id')
            //            };
            //            self.persister.battle.defend(data, function () {
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            });
            //        });

            //        wrapper.on('click', '#view-scores #btn-view-scores', function () {
            //            self.persister.user.scores(function (scores) {
            //                var html = ui.scoresList(scores);
            //                $('#scores').html(html);
            //            }, function (err) {
            //                wrapper.find("#error-messages").text(err.responseJSON.Message);
            //            })
            //        });
        },
        updateUI: function (contentSelector, loginSelector) {
            //TODO: Make update ui
            this.loadUI(contentSelector, loginSelector);

            //this.persister.game.open(function (games) {
            //    var list = ui.openGamesList(games);
            //    $(selector + " #open-games")
            //        .html(list);
            //});
            //this.persister.game.myActive(function (games) {
            //    var list = ui.activeGamesList(games);
            //    $(selector + " #active-games")
            //        .html(list);
            //});
            //this.persister.message.unread(function (msg) {
            //    var msgList = ui.messagesList(msg);
            //    $(selector + " #messages-holder").html(msgList);
            //});
            //var gameId = $('#game-state-container').first().data('game-id');
            //if (gameId) {
            //    this.persister.game.field(gameId, function (gameField) {
            //        var gameHtml = ui.gameField(gameField);
            //        $(selector + " #game-holder").html(gameHtml);
            //    }, function () {

            //    });
            //}
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