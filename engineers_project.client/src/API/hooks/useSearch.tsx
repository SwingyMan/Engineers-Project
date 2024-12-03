import { useState } from "react";
import { UserDTO } from "../DTO/UserDTO";
import { useQuery } from "@tanstack/react-query";
import {
  fetchPostByTitle,
  fetchUserByName,
} from "../services/searchByName.service";

export const useSearch = (searchQuery: string) => {
  const searchUser = () =>
    useQuery({
      queryKey: ["searchUser"],
      queryFn: () => fetchUserByName(searchQuery),
    });
  const searchPost = () =>
    useQuery({
      queryKey: ["searchPost"],
      queryFn: () => fetchPostByTitle(searchQuery),
    });

  const {
    data: user,
    isError: userError,
    isFetching: userFeching,
  } = searchUser();
  const {
    data: post,
    isError: postError,
    isFetching: postFeching,
  } = searchPost();

  const searchResult = {
    users: user,
    posts: post,
  };
  return {
    searchResult,
    userError,
    userFeching,
    postFeching,
    postError,
  };
};
