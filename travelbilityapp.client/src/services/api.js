import { getLoggedInUserAccessToken } from "../utils/auth-utils";

const host = import.meta.env.VITE_API_URL;

export async function requester(method, url, body) {
    const options = {
        method,
        headers: {},
    };

    if (body) {
        options.headers['content-type'] = 'application/json';
        options.body = JSON.stringify(body);
    }

    const loggedInUserAccessToken = getLoggedInUserAccessToken();

    if (loggedInUserAccessToken) {
        options.headers['Authorization'] = `Bearer ${loggedInUserAccessToken}`;
    }

    const request = await fetch(`${host}/${url}`, options);
    const response = await request.json();

    if (!request.ok) {
        debugger
        console.log(response);

        throw { status: request.status, errorsData: response };
    }

    return response;
}

export const get = requester.bind(null, 'GET');
export const post = requester.bind(null, 'POST');
export const put = requester.bind(null, 'PUT');
export const del = requester.bind(null, 'DELETE');