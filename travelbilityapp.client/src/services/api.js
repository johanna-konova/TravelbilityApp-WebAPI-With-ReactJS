import { getLoggedInUserAccessToken } from "../utils/auth-utils";
import { triggerError } from "../navigation";

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
    debugger
    let response;

    try {
        response = await request.json()
    } catch {
        response = null;
    }

    if (!request.ok) {
        debugger
        console.log(response);

        triggerError({ status: request.status, errorsData: response });
        throw { status: request.status, errorsData: response };
    }

    return response;
}

export const get = requester.bind(null, 'GET');
export const post = requester.bind(null, 'POST');
export const put = requester.bind(null, 'PUT');
export const del = requester.bind(null, 'DELETE');