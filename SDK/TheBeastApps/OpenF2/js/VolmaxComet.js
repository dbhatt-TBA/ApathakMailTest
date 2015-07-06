var volmaxcomet = function() {
    var _premdataSubscription;
    var _strikedataSubscription;
    var _voldataSubscription;
    var _otherdataSubscription;
    var _metaSubscriptions = [];
    var _cometd;
    var _username;
    var _disconnecting;

    return {
        init: function(cometd, username, password, sessionID) {

            
            _cometd = cometd;
            _username = username;

            _metaSubscribe();

            _cometd.configure({ url: 'comet.axd' });

            _cometd.handshake({
                ext: {
                    authentication: {
                        user: username,
                        credentials: password,
                        customerID: password,
                        sessionID: sessionID
                    }
                }
            });
        }

        , leave: function() {
           
            _unsubscribe();
            _cometd.disconnect();

            _metaUnsubscribe();
            _disconnecting = true;
        }

    }

    function _unsubscribe() {
        if (_premdataSubscription)
            _cometd.unsubscribe(_premdataSubscription);
        _premdataSubscription = null;

        if (_strikedataSubscription)
            _cometd.unsubscribe(_strikedataSubscription);
        _strikedataSubscription = null;

        if (_voldataSubscription)
            _cometd.unsubscribe(_voldataSubscription);
        _voldataSubscription = null;

        if (_otherdataSubscription)
            _cometd.unsubscribe(_otherdataSubscription);
        _otherdataSubscription = null;
    }

    function _subscribe() {
        _unsubscribe();
        _premdataSubscription = _cometd.subscribe('/service/premdata', this, handleIncomingMessageFromBeast);
        _strikedataSubscription = _cometd.subscribe('/service/strikedata', this, handleIncomingMessageFromBeast);
        _voldataSubscription = _cometd.subscribe('/service/voldata', this, handleIncomingMessageFromBeast);
        _otherdataSubscription = _cometd.subscribe('/service/otherdata', this, handleIncomingMessageFromBeast);
        
        ///service/voldata
    }

    function _metaUnsubscribe() {
        for (var subNumber in _metaSubscriptions) {
            _cometd.removeListener(_metaSubscriptions[subNumber]);
        }
        _metaSubscriptions = [];
    }

    function _metaSubscribe() {
        _metaUnsubscribe();
        _metaSubscriptions.push(_cometd.addListener('/meta/handshake', this, _metaHandshake));
        _metaSubscriptions.push(_cometd.addListener('/meta/connect', this, _metaConnect));
        _metaSubscriptions.push(_cometd.addListener('/meta/unsuccessful', this, _metaUnsuccessful));
    }

    function _metaHandshake(message) {
        _connected = false;
        _premdataSubscription = null;
        _strikedataSubscription = null;
        _voldataSubscription = null;
        _otherdataSubscription = null;
        handleIncomingMessage({ data: { message: "Handshake complete. Successful? " + message.successful} });
    }

    function _connectionEstablished() {
        handleIncomingMessage({
            data: {
                message: 'Connection to Server Opened'
            }
        });
        _cometd.batch(function() {
            _subscribe();
        });
    }

    function _connectionBroken() {
        handleIncomingMessage({
            data: {
                message: 'Connection to Server Broken'
            }
        });
    }

    function _connectionClosed() {
        handleIncomingMessage({
            data: {
                message: 'Connection to Server Closed'
            }
        });
    }

    var _connected = false;
    function _metaConnect(message) {
        if (_disconnecting) {
            _connected = false;
            _connectionClosed();
        }
        else {
            var wasConnected = _connected;
            _connected = message.successful === true;
            if (!wasConnected && _connected) {
                _connectionEstablished();
            }
            else if (wasConnected && !_connected) {
                _connectionBroken();
            }
        }
    }

    function _metaUnsuccessful(message) {
        handleIncomingMessage({ data: { message: "Request on channel " + message.channel + " failed: " + (message.error == undefined ? "No message" : message.error)} });
    }

} ();
