(function () {

    var output = document.querySelector('.output'),
        input = document.querySelector('input'),
        button = document.querySelector('button'),
        avatar = document.querySelector('.avatar'),
        presence = document.querySelector('.presence');
    var channel = 'simple-chat';

    // Assign a random avatar in random color
    avatar.className = 'face-' + ((Math.random() * 13 + 1) >>> 0) + ' color-' + ((Math.random() * 10 + 1) >>> 0);

})();