import { useEffect, useState } from "react";
import { useAuth } from "../Router/AuthProvider";
import styled from "styled-components";
import { getGroupImg, getUserImg, postAttachment } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { useGroupDetails } from "../API/hooks/useGroup";
import { useNavigate, useParams } from "react-router";

import { IoIosArrowDown, IoIosArrowUp } from "react-icons/io";

interface FormData {
  groupID: string;
  groupName: string;
  groupDescription: string;
  image: File | null;
  preview: string | null;
}
const StyledEditGroup = styled.div`
  color: var(--white);
  display: flex;
  width: 100%;
`;
const EditImage = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;
const Input = styled.input`
  padding: 10px;
  background-color: #84b3e9cc;
  font-size: 1rem;
  border: 1px solid #ccc;
  &::placeholder {
    color: var(--white);
  }
  border-radius: 4px;
  color: var(--white);
`;
const GroupEditWrapper = styled.div`
  flex: 1;
`;

const GroupData = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-start;
`;
const TextArea = styled.textarea`
  padding: 10px;
  font-family: inherit;
  font-size: inherit;
  background-color: #84b3e9cc;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  resize: none;
  height: 100px;
  color: var(--white);
  &::placeholder {
    color: var(--white);
  }
`;
const StyledForm = styled.form`
  display: flex;
  width: 100%;
`;
const TextInputsWrapper = styled.div`
  flex-direction: column;
  display: flex;
  width: 100%;
`;
const Button = styled.button`
  padding: 10px 15px;
  font-size: 1rem;
  border: none;
  width: 20%;
  border-radius: 4px;
  cursor: pointer;
  background-color: #007bff;
  color: var(--white);
  &:hover {
    background-color: #0056b3;
  }
`;
const DeleteButton = styled.button`
  padding: 10px 15px;
  font-size: 1rem;
  border: none;
  width: 20%;
  border-radius: 4px;
  cursor: pointer;
  background-color: #dd3d3d;
  color: var(--white);
  &:hover {
    background-color: rgba(255, 136, 136, 0.75);
  }
`;
const ButtonWrapper = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
`;
const GroupUsersWrapper = styled.div`
  display: flex;
  flex-direction: column;
  gap: 5px;
  padding: 16px;
`;
const UserHeaders = styled.div`
  display: flex;
  width: 100%;
  background-color: #6085afcc;
  border-radius: 10px;
`;
const UserHeader = styled.div<{ isActive: boolean }>`
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: ${(props) =>
    props.isActive ? "var(--whiteTransparent20)" : ""};
  width: 100%;
  padding: 16px;
  &:first-of-type {
    border-radius: 10px 0 0 10px;
  }
  &:last-of-type {
    border-radius: 0 10px 10px 0;
  }
`;
const UserItem = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;
const HeaderInfo = styled.div`
  display: flex;
  gap: 4px;
  cursor: pointer;
`;
export function EditGroupPage() {
  const { id } = useParams();
  console.log(id);
  const { user } = useAuth();
  const navigate = useNavigate();
  const { data: groupData, handleEditGroup } = useGroupDetails(id!);
  useEffect(() => {
    if (groupData) {
      if (user) {
        if (
          groupData.groupUsers.find((guser) => guser.user.id === user.id)
            ?.isOwner
        ) {
        } else {
          navigate(`/group/${id}`);
        }
      }
    }
  });
  const [openUsers, setOpenUsers] = useState(false);
  const [formData, setFormData] = useState<FormData>({
    groupID: id!,
    groupName: groupData?.name!,
    groupDescription: groupData?.description!,
    image: null,
    preview: null,
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value, files } = e.target as HTMLInputElement &
      HTMLTextAreaElement;
    if (name === "image" && files) {
      const file = files[0];
      setFormData({
        ...formData,
        image: file,
        preview: file ? URL.createObjectURL(file) : null,
      });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const editImage = () => {
    const data = new FormData();
    data.append("file", formData.image!);
    postAttachment("User/AddAvatar", data);
  };

  const handleSubmit = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    if (formData.image !== null) {
      editImage();
    }
    if (
      formData.groupDescription !== groupData?.description ||
      formData.groupName !== groupData?.name
    ) {
      const editedGroup = {
        groupId: formData.groupID,
        groupName: formData.groupName,
        groupDescription: formData.groupDescription,
      };

      handleEditGroup.mutate(editedGroup);
    }
  };

  return (
    <GroupEditWrapper>
      <GroupData>
        <StyledForm onSubmit={handleSubmit}>
          <StyledEditGroup>
            <EditImage>
              <p>Image Preview:</p>
              <ImageDiv
                width={120}
                url={
                  formData.preview
                    ? formData.preview
                    : getGroupImg(groupData?.id!)
                }
              />
              <input
                type="file"
                id="image"
                name="image"
                accept=".jpg, .jpeg, .png"
                size={50}
                onChange={handleChange}
              />
            </EditImage>
            <TextInputsWrapper>
              <label htmlFor="groupName">Group Name:</label>

              <Input
                type="text"
                id="groupName"
                name="groupName"
                value={formData.groupName}
                onChange={handleChange}
                placeholder={groupData?.name}
              />
              <br />

              <label htmlFor="groupDescription">Group description:</label>

              <TextArea
                id="groupDescription"
                name="groupDescription"
                value={formData.groupDescription}
                placeholder={groupData?.description}
                onChange={handleChange}
              />
              <br />
              <ButtonWrapper>
                <DeleteButton>Delete Group</DeleteButton>
                <Button type="submit">Submit</Button>
              </ButtonWrapper>
            </TextInputsWrapper>
          </StyledEditGroup>
        </StyledForm>
      </GroupData>
      <GroupUsersWrapper>
        <UserHeaders>
          <UserHeader
            onClick={() => setOpenUsers(!openUsers)}
            isActive={!openUsers}
          >
            Group Users
            {!openUsers ? (
              <IoIosArrowUp size={20} />
            ) : (
              <IoIosArrowDown size={20} />
            )}
          </UserHeader>
          <UserHeader
            onClick={() => setOpenUsers(!openUsers)}
            isActive={openUsers}
          >
            Awaiting Users{" "}
            {`(${
              groupData?.groupUsers.filter((u) => u.isAccepted === false).length
            })`}
            {openUsers ? (
              <IoIosArrowUp size={20} />
            ) : (
              <IoIosArrowDown size={20} />
            )}
          </UserHeader>
        </UserHeaders>
        {groupData &&
          (!openUsers
            ? groupData.groupUsers
                .filter((u) => u.isAccepted)
                .map((u) => (
                  <UserItem>
                    <HeaderInfo>
                      <ImageDiv
                        width={40}
                        url={
                          u.user.avatarFileName
                            ? getUserImg(u.user.avatarFileName)
                            : ""
                        }
                      />
                      {u.user.username}
                    </HeaderInfo>
                    {user?.id !== u.user.id && (
                      <DeleteButton>Remove from group</DeleteButton>
                    )}
                  </UserItem>
                ))
            : groupData.groupUsers
                .filter((u) => u.isAccepted === false)
                .map((u) => (
                  <UserItem>
                    <HeaderInfo>
                      <ImageDiv
                        width={40}
                        url={
                          u.user.avatarFileName
                            ? getUserImg(u.user.avatarFileName)
                            : ""
                        }
                      />
                      {u.user.username}
                    </HeaderInfo>
                    <Button>Accept to Group</Button>
                  </UserItem>
                )))}
      </GroupUsersWrapper>
    </GroupEditWrapper>
  );
}
