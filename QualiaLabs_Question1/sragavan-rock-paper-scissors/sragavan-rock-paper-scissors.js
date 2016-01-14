CurrentGames = new Mongo.Collection('gmaes');

var intial_game_data = {
  player1: {
    last_active: 0,
    has_action: false,
    action: false
  },
  player2: {
    last_active:0,
    has_action: false,
    action: false
  },
  result: {}
};

if (Meteor.isClient) {

  Router.route('/', function() {

    this.render('start');

    // clear session heartbeat, if set
    clearInterval(Session.get('heartbeat'));
  });

  // start landing page
  Template.start.helpers({
    openrooms: function() {
      // just get top game
      return CurrentGames.find({}, {limit: 1});
    }
  });

  Template.start.events({
    'click button.new': function() {
      var game = CurrentGames.insert(intial_game_data);
      Router.go('/' + game + '/player1');
    }
  });

  // game room
  Router.route('/:gameID/:player', function() {
    var gameID = this.params.gameID;
    var player = this.params.player;

    // determine game room
    var game = CurrentGames.findOne(gameID, {reactive: false});
    if (!game) {
      return Router.go('/');
    }

    // make sur either player 1 or player 2
    if (!player.match(/^player[12]$/)) {
      return Router.go('/');
    }

    // update player's activity indicator
    Session.set('heartbeat', setInterval(function() {
      var update = {$set: {}};
      update.$set[player + '.last_active'] = new Date();
      CurrentGames.update(game._id, update);
    }, 1000)); // every second

    // render game template
    this.render('game', {
      data: function() { return {
        gameId: game._id,
        player: player
      }; }
    });
  });

  Template.game.helpers({
    game: function(gameId) {
      return CurrentGames.findOne(gameId);
    }
  });

  Template.game.events({
    'click button': function(ev) {
      var move = ev.currentTarget.dataset.type;
      var data = Template.currentData();

      var updates = {$set: {}};
      updates.$set[data.player + '.has_action'] = true;
      updates.$set[data.player + '.action'] = move;

      CurrentGames.update(data.gameId, updates);
    }
  });
}

if (Meteor.isServer) {
  Meteor.startup(function() {

    var victory = {
      'rock': 'scsr',
      'papr': 'rock',
      'scsr': 'papr',
    };
    CurrentGames.find({}).observe({
      changed: function(game) {
        var p1 = game.player1,
            p2 = game.player2;

        // ignore if waiting on other player
        if (!(p1.has_action && p2.has_action)) return;

        // determine game winner
        game.result = {
          player1: p1.action,
          player2: p2.action,
          winner: (p1.action == p2.action) ? 'tie' :
            ((victory[p1.action] == p2.action) ? 'player1' : 'player2')
        };

        // clear current game state,
        p1.action = p1.has_action = false;
        p2.action = p2.has_action = false;

        CurrentGames.update(game._id, game);
      }
    });
  });
}
