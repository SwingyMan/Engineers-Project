

const getHost = () => {
    if (import.meta.env.PROD) {
        const url = new URL(window.location.href);
        return `${url.protocol}//${url.host}/`;
    } else {
        return import.meta.env.VITE_HOST_DEV;
    }
};

export const getToken = (): string | null => {
    return localStorage.getItem('polsl-social');
};

 export const getHeaders = (formdata?:boolean) => {
    const token = getToken();
    if(formdata){
        return{
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
        }
    }
    else{
    return {
        'Content-Type': 'application/json',
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
    };}
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

export const postAttachment = async <T>(url:string, data: FormData):Promise<T>=>{
    const response =await fetch(`${getHost()}${url}`, {
        method: 'POST',
        headers: getHeaders(true),
        body: data,
    })
    if (!response.ok){
        throw new Error(`Posting Attachment has failed: ${response.status}`)
    }
    
    return response.json();
}

export const put = async (url: string, data: unknown) => {


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
export const patch = async <T>(url: string, data: unknown): Promise<T> => {
    const response = await fetch(`${getHost()}${url}`, {
        method: "PATCH",
        headers: getHeaders(),
        body: JSON.stringify(data),
    })
    if (!response.ok) {
        throw new Error(`PATCH request failed: ${response.status}`);
    }
    return response.json()
}


export const del = async <T>(url: string): Promise<T> => {
    const response = await fetch(`${getHost()}${url}`, {
        method: "DELETE",
        headers: getHeaders()
    });
    if (!response.ok) {
        throw new Error(`DELETE request failed: ${response.status}`);
    }


    return response.json()
}
export const getUserImg = (url: string) => {
    const src = `${getHost()}User/GetAvatar?FileName=${url}`
return src

}
export const getGroupImg = (id:string) =>{
    const src = `${getHost()}Group/GetGroupImageById?groupId=${id}`
    return src
}
export const getAttachment = async (id:string) =>{
    const response =await fetch( `${getHost()}Attachment/GetFile/${id}`)
    return response
}