const gabletGetPageParams = () => {
    return window.location.href;
}

/**
 * Updates the url of the current page to the specified url.
 * 
 * @param {string} uri The URL to set the window location to.
 */
const gabletPushUrl = (uri) => {
    window.history.pushState({ old_location: window.location.href }, "", uri);
}

const gabletOnPopUrl = (callback) => {
    addEventListener('popstate', (ev) => {
        console.log(ev);
        callback();
    });
}

window.gabletGetPageParams = gabletGetPageParams;
window.gabletPushUrl = gabletPushUrl;
window.gabletOnPopUrl = gabletOnPopUrl;