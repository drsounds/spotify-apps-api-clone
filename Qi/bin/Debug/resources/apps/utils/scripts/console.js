console = {
    log: function () {
        document.querySelector('#console').innerHTML = new Date() + ' ' + arguments[0] + '\n' + document.querySelector('#console').innerHTML;
    }
};