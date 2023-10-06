const testing = () => {
    console.log("Hello Gablet");
};

const gabletStorageGet = (key) => {
    return localStorage.getItem(key);
}

const gabletStorageSet = (key, value) => {
    localStorage.setItem(key, value);
}

window.testing = testing;
window.gabletStorageGet = gabletStorageGet;
window.gabletStorageSet = gabletStorageSet;