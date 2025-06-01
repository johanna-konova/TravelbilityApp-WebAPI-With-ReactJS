export function getLoggedInUserAccessToken() {
    const loggedInUserData = localStorage.getItem('userData');    

    return JSON.parse(loggedInUserData)?.accessToken;
}