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

    // TODO: Make Main Content UI
    function buildMainUI() {
        var html = '<h2>Recent News</h2>';
        //    '<div><span id="user-nickname">Hello, ' + nickname + '</span>\
        //<button id="btn-logout">Logout</button></div>\
        //<div id="create-game-holder">\
        //    Create new game: \
        //	Title: <input type="text" id="tb-create-title" />\
        //	Password: <input type="text" id="tb-create-pass" />\
        //	<button id="btn-create-game">Create</button>\
        //</div>\
        //<div id="view-scores">\
        //    <button id="btn-view-scores">View Scores</button>\
        //    <div id="scores"></div>\
        //</div>\
        //<div id="open-games-container">\
        //	<h2>Open</h2>\
        //	<div id="open-games"></div>\
        //</div>\
        //<div id="active-games-container">\
        //	<h2>Active</h2>\
        //	<div id="active-games"></div>\
        //</div>\
        //<div id="game-holder"></div>\
        //<div id="messages-holder">\
        //</div>';
        return html;
    }

    // TODO: Make Article Content UI
    function buildArticleUI() {
        var html = 'article content ui';
        //    '<div><span id="user-nickname">Hello, ' + nickname + '</span>\
        //<button id="btn-logout">Logout</button></div>\
        //<div id="create-game-holder">\
        //    Create new game: \
        //	Title: <input type="text" id="tb-create-title" />\
        //	Password: <input type="text" id="tb-create-pass" />\
        //	<button id="btn-create-game">Create</button>\
        //</div>\
        //<div id="view-scores">\
        //    <button id="btn-view-scores">View Scores</button>\
        //    <div id="scores"></div>\
        //</div>\
        //<div id="open-games-container">\
        //	<h2>Open</h2>\
        //	<div id="open-games"></div>\
        //</div>\
        //<div id="active-games-container">\
        //	<h2>Active</h2>\
        //	<div id="active-games"></div>\
        //</div>\
        //<div id="game-holder"></div>\
        //<div id="messages-holder">\
        //</div>';
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

    //function buildOpenGamesList(games) {
    //    var list = '<ul class="game-list open-games">';
    //    for (var i = 0; i < games.length; i++) {
    //        var game = games[i];
    //        list +=
    //			'<li data-game-id="' + game.id + '">' +
    //				'<a href="#" >' +
    //					$("<div />").html(game.title).text() +
    //				'</a>' +
    //				'<span> by ' +
    //					game.creator +
    //				'</span>' +
    //			'</li>';
    //    }
    //    list += "</ul>";
    //    return list;
    //}

    //function buildActiveGamesList(games) {
    //    var gamesList = Array.prototype.slice.call(games, 0);
    //    gamesList.sort(function (g1, g2) {
    //        if (g1.status == g2.status) {
    //            return g1.title > g2.title;
    //        }
    //        else {
    //            if (g1.status == "in-progress") {
    //                return -1;
    //            }
    //        }
    //        return 1;
    //    });

    //    var list = '<ul class="game-list active-games">';
    //    for (var i = 0; i < gamesList.length; i++) {
    //        var game = gamesList[i];
    //        list +=
    //			'<li class="game-status-' + game.status + '" data-game-id="' + game.id + '" data-creator="' + game.creator + '">' +
    //				'<a href="#" class="btn-active-game">' +
    //					$("<div />").html(game.title).text() +
    //				'</a>' +
    //				'<span> by ' +
    //					game.creator +
    //				'</span>' +
    //			'</li>';
    //    }
    //    list += "</ul>";
    //    return list;
    //}

    //function buildGameTable(gameField) {
    //    var units = gameField.red.units;
    //    for (var i = 0; i < gameField.blue.units.length; i++) {
    //        units.push(gameField.blue.units[i]);
    //    }
    //    var tableHtml =
    //		'<table border="1" cellspacing="0" cellpadding="0">';
    //    for (var i = 0; i < 9; i++) {
    //        tableHtml += '<tr>';
    //        for (var j = 0; j < 9; j++) {
    //            tableHtml += '<td>';

    //            var hasUnit = false;
    //            for (var k = 0; k < units.length; k++) {
    //                if (units[k].position.x == j && units[k].position.y == i) {
    //                    tableHtml +=
    //                        '<div class="game-unit \
    //                                game-unit-' + units[k].owner + ' ' +
    //                                (gameField.inTurn == units[k].owner ? 'in-turn' : '') +
    //                                '" data-unit-id="' + units[k].id + '"\
    //                                 data-position="' + j + ' ' + i + '" >\
    //                            <p>' + units[k].type + '</p>\
    //                            <p>Hit Points: ' + units[k].hitPoints + '</p>\
    //                            <button class="unit-defend-action" style="display: none">Defend</button>\
    //                        </div>';
    //                    hasUnit = true;
    //                    break;
    //                }
    //            }

    //            if (!hasUnit) {
    //                tableHtml += '<div class="empty" ' +
    //                    ' data-position="' + j + ' ' + i + '" ></div>';
    //            }

    //            tableHtml += '</td>';
    //        }

    //        tableHtml += '</tr>';
    //    }

    //    tableHtml += '</table>';
    //    return tableHtml;
    //}

    //function buildGameField(gameField) {
    //    var inTurn = (gameField.inTurn == 'red' ? gameField.red.nickname : gameField.blue.nickname);
    //    var html =
    //        '<div id="game-state-container" data-game-id="' + gameField.gameId + '">\
    //            <h2>' + gameField.title + '</h2>\
    //            <p id="game-info">Turn: ' + gameField.turn + ', In Turn: ' + inTurn + '</p>\
    //            <div id="error-messages"></div>\
    //			<div id="game-field">' +
    //            buildGameTable(gameField) +
    //        '</div>' +
    //    '</div>';
    //    return html;
    //}

    //function buildScoresList(scores) {
    //    var html = '<table border="1" cellspacing="0" cellpadding="0">';
    //    html += '<tr>\
    //            <th>Nickname</th>\
    //            <th>Score</th>\
    //            </tr>';
    //    for (var i = 0; i < length; i++) {
    //        html += '<tr>\
    //            <td>' + scores[i].nickname + '</td>\
    //            <td>' + scores[i].score + '</td>\
    //            </tr>';
    //    }
    //    html += '</table>';

    //    return html;
    //}

    //function buildMessagesList(messages) {
    //    var list = '<ul class="messages-list">';
    //    var msg;
    //    for (var i = 0; i < messages.length; i += 1) {
    //        msg = messages[i];
    //        var item =
    //			'<li>' +
    //				'<a href="#" class="message-state-' + msg.state + '">' +
    //					msg.text +
    //				'</a>' +
    //			'</li>';
    //        list += item;
    //    }
    //    list += '</ul>';
    //    return list;
    //}

    return {
        mainUI: buildMainUI,
        articleUI: buildArticleUI,
        welcomeUI: buildWelcomeUI,
        registerUI: buildRegisterUI,
        //gameUI: buildGameUI,
        //openGamesList: buildOpenGamesList,
        loginForm: buildLoginForm,
        //activeGamesList: buildActiveGamesList,
        //gameField: buildGameField,
        //messagesList: buildMessagesList,
        //scoresList: buildScoresList
    }

}());