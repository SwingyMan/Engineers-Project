const getHost = () => {
	if (import.meta.env.PROD) {
		const url = new URL(window.location.href);
		return `${url.protocol}//${url.host}/`;//TODO dodać link 
	} else {
		return import.meta.env.VITE_HOST_DEV;
	}
};

const getToken = (): string | null => {
	return localStorage.getItem('token');
};

const getHeaders = () => {
	const token = getToken();
	return {
		'Content-Type': 'application/json',
		...(token ? { Authorization: `Bearer ${token}` } : {}),
	};
};

export const get = async <T>(url: string): Promise<T> => {
	const response = await fetch(`${getHost()}${url}`, {
		method: 'GET',
		headers: getHeaders(),
	});

	if (!response.ok) {
		throw new Error(`GET request failed: ${response.status}`);
	}

	return response.json();
};

export const post = async <T>(url: string, data: unknown): Promise<T> => {
	const response = await fetch(`${getHost()}${url}`, {
		method: 'POST',
		headers: getHeaders(),
		body: JSON.stringify(data),
	});

	if (!response.ok) {
		throw new Error(`POST request failed: ${response.status}`);
	}

	return response.json();
};

export const put = async <T>(url: string, data: unknown): Promise<T> => {
	const response = await fetch(`${getHost()}${url}`, {
		method: "PUT",
		headers: getHeaders(),
		body: JSON.stringify(data),
	});
	if (!response.ok) {
		throw new Error(`PUT request failed: ${response.status}`);
	}

	return response.json()
};

export const del = async <T>(url:string):Promise<T>=>{
	const response = await fetch(`${getHost()}${url}`, {
		method: "DELETE",
		headers: getHeaders()
	});
	if (!response.ok) {
		throw new Error(`DELETE request failed: ${response.status}`);
	}

	return response.json()
}
