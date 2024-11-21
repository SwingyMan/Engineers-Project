import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"
import { createPost, deletePost, editPost, fetchPosts } from "../services/posts.service";

export const usePosts = () => {

    const QueryKey = ['post']
    const queryClient = useQueryClient();
    const PostQuery = () => {
        return useQuery({ queryKey: QueryKey, queryFn: fetchPosts ,staleTime:(60*1000)})
    }
    const { isError, isFetched, data, error, isPending } = PostQuery()
    const handleAddPost = useMutation({
        mutationFn: async (newPost:{}) => {
            await createPost(newPost)
        },
        onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey })
        //onError
    })
    const handleEditPost = useMutation({
        mutationFn: async (editedPost:Partial<PostDTO>) => {
            await editPost(editedPost)
        },
        onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey })
    })
    const handleDeletePost = useMutation({
        mutationFn: async (id: string) => {
            await deletePost(id)
        },
        onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey })
    })

    return {
        data,
        handleAddPost,
        handleEditPost,
        handleDeletePost,
        error,
        isError,
        isFetched,
        isPending
    }
}