import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  createComment,
  deleteComment,
  editComment,
  fetchComments,
} from "../services/comments.service";
import { UUID } from "crypto";

export const useComments = (id: UUID) => {
  const QueryKey = ["comments",id];
  const queryClient = useQueryClient();
  const CommentQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchComments(id),
      staleTime: 60 * 1000,
    });
  };
  const { isError, isFetched, data, error, isPending } = CommentQuery();
  const handleAddComment = useMutation({
    mutationFn: async (newComment: Partial<CommentDTO>) => {
      await createComment(newComment);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey })
    },
    onError: (e)=>{
      console.log(e)
    }
  });
  const handleEditComment = useMutation({
    mutationFn: async (editedComment: Partial<CommentDTO>) => {
      await editComment(editedComment);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });
  const handleDeleteComment = useMutation({
    mutationFn: async (id: string) => {
      await deleteComment(id);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });

  return {
    data,
    handleAddComment,
    handleEditComment,
    handleDeleteComment,
    error,
    isError,
    isFetched,
    isPending,
  };
};
