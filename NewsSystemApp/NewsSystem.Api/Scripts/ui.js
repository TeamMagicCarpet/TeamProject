var ui = (function () {

    function buildLoginForm() {
        var html =
                '<button id="login-button" class="btn">Log In</button>\
                <div id="login-popup" style="display:none">\
                    <form>\
                        <label for="tb-login-username">Username:</label>\
                        <input type="text" id="tb-login-username" /><br />\
                        <label for="tb-login-password">Password:</label>\
                        <input type="password" id="tb-login-password" /><br />\
                        <button id="submit-login-button" class="btn">Log In</button>\
                        <button id="reg-button" class="btn">Register</button>\
                    </form>\
                </div>';

        return html;
    }

    function buildMainUI(articles) {
        var html =
            '<h2>Recent News</h2>\
            <ul id="recent-news-list">';

        for (var i = 0; i < 10 && i < articles.length; i++) {
            html += '<li>\
                <a class="article-title" data-articleId="' + articles[i].ArticleId + '">' + articles[i].Title + '</a>\
                <p class="article-meta">from ' + articles[i].AuthorName + ' on ' + articles[i].CreationDate + '</p>\
                <p class="article-content">' + articles[i].Content + '</p>\
            </li>';
        }
        html += '</ul>'

        return html;
    }

    function buildArticleUI(article) {
        var comments = buildCommentsUI(article.Comments);
        var html =
            '<button id="back-button" class="btn">Back</button>\
            <h3>' + article.Title + '</h3>\
            <p>from ' + article.AuthorName + ' on ' + article.CreationDate + ', rating: ' + article.Rating + '</p>\
            <p>' + article.Content + '</p>\
            <form id="placeVote">\
                <label>Rate article: </label>\
                <select id="rating">\
                    <option value="1">1</option>\
                    <option value="2">2</option>\
                    <option value="3">3</option>\
                    <option value="4">4</option>\
                    <option value="5">5</option>\
                    <option value="6">6</option>\
                    <option value="7">7</option>\
                    <option value="8">8</option>\
                    <option value="9">9</option>\
                    <option value="10">10</option>\
                </select>\
                <button id="vote-button" class="btn">Submit Rating</button>\
            </form>\
            <button class="btn answer-comment">Comment</button>\
            <div id="comment-answer-box" style="display: none">\
                <form>\
                    <label for="tb-comment-content">Content:</label>\
                    <input type="text" id="tb-comment-content" />\
                    <button id="submit-comment" class="btn">Done</button>\
                </form>\
            </div>\
            <div>' + comments + '</div>';

        return html;
    }

    function buildCommentsUI(comments) {
        var html = "<ul>";
        for (var comment in comments) {
            html += buildCommentList(comments[comment]);
        }
        html += "</ul>";

        return html;
    }

    function buildCommentList(comment) {
        var html = '<li>\
                    <p>' + comment.User.UserName + '</p>\
                    <p>' + comment.Content + '</p>\
                    <button class="btn answer-comment" data-answerId="' + comment.CommentId + '">Answer</button>\
                    <div id="comment-answer-box" style="display: none">\
                        <form>\
                            <label for="tb-comment-content">Content:</label>\
                            <input type="text" id="tb-comment-content" />\
                            <button id="submit-answer" class="btn">Done</button>\
                        </form>\
                    </div>';
        if (comment.Answers.length > 0) {
            html += "<ul>";
            for (var answer in comment.Answers) {
                html += buildCommentList(comment.Answers[answer]);
            }
            html += "</ul>";
        }

        html += "</li>";

        return html;
    }

    function buildWelcomeUI(username) {
        var html = '<p>Welcome back, ' + username + '<p>\
                    <button id="logout-button" class="btn">Log out</button>';
        return html;
    }

    function buildRegisterUI() {
        var html =
            '<div id="register-form">\
                <form>\
                    <label for="tb-reg-firstname">First Name:</label>\
                    <input type="text" id="tb-reg-firstname" /><br />\
                    <label for="tb-reg-lastname">Last Name:</label>\
                    <input type="text" id="tb-reg-lastname" /><br />\
                    <label for="tb-reg-username">Username:</label>\
                    <input type="text" id="tb-reg-username" /><br />\
                    <label for="tb-reg-password">Password:</label>\
                    <input type="password" id="tb-reg-password" /><br />\
                    <label for="tb-reg-reppassword">Repeat Password:</label>\
                    <input type="password" id="tb-reg-reppassword" /><br />\
                    <label for="tb-reg-email">Email:</label>\
                    <input type="email" id="tb-reg-email" /><br />\
                    <button id="submit-reg-button" class="btn">Register</button>\
                </form>\
            </div>';

        return html;
    }

    function buildNewArticleUI() {
        var html =
            '<div id="new-article-form">\
                <form id="new-article" enctype="multipart/form-data">\
                    <label for="tb-article-title">Title:</label>\
                    <input type="text" id="tb-article-title" />\
                    <div>\
                        <label for="area-article-content">Content:</label>\
                        <textarea id="area-article-content"></textarea>\
                    </div>\
                    <label>Picture:</label>\
                    <input id="file-select" name="file" type="file" />\
                    <button id="submit-article" class="btn">Post Article</button>\
                </form>\
            </div>';
        return html;
    }

    return {
        mainUI: buildMainUI,
        articleUI: buildArticleUI,
        welcomeUI: buildWelcomeUI,
        registerUI: buildRegisterUI,
        newArticleUI: buildNewArticleUI,
        loginForm: buildLoginForm,
    }

}());