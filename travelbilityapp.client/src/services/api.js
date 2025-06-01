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
    debugger
    if (!request.ok) {
        const errorData = await request.json();

        if ((request.status === 400 ||
             request.status === 409 ||
             request.status === 401)) {
            const messages = Object
                .values(errorData)
                .flat();
            debugger
            throw { status: request.status, messages };
        }

        if (request.status === 403) {
            //auth.removeUserData();
        }
        
        throw await request.json();
    }

    if (request.status !== 204) {
        return await request.json();
    }
}

export const get = requester.bind(null, 'GET');
export const post = requester.bind(null, 'POST');
export const put = requester.bind(null, 'PUT');
export const del = requester.bind(null, 'DELETE');