import { useState } from "react";
import { useAuth } from "../Router/AuthProvider";
import styled from "styled-components";
import { getUserImg, postAttachment } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { editUserById } from "../API/services/user.service";
import { VscEye, VscEyeClosed } from "react-icons/vsc";
import { FaP } from "react-icons/fa6";

interface FormData {
  Username: string;
  Password: string;
  image: File | null;
  preview: string | null;
}
const StyledEditUser = styled.div`
  color: var(--white);
  display: flex;
  width: 100%;
  justify-content: center;
`;
const EditImage = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;
const ProfileEditWrapper = styled.div`
  flex: 1;
`;
const ProfileData = styled.div`
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
const StyledForm = styled.form`
  display: flex;
  width: 100%;
`;
const Input = styled.input`
  padding: 10px;
  background-color: #84b3e9cc;
  font-size: 1rem;
  width: 100%;
  border: 1px solid #ccc;
  &::placeholder {
    color: var(--white);
  }
  border-radius: 4px;
  color: var(--white);
`;
const Button = styled.button`
  padding: 10px 15px;
  font-size: 1rem;
  border: none;
  width: fit-content;
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
  width: fit-content;
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
const TextInputsWrapper = styled.div`
  display: flex;
  max-width: 500px;
  width: 100%;
  flex-direction: column;
  gap: 5px;
`;
const InputWrapper = styled.div`
  display: flex;
  position: relative;
  width: 100%;
  margin-bottom: 1em;
`;
const EyeIcon = styled.div`
  position: absolute;
  right: 0px;
  padding-right: 12px;
  top: 7px;
  cursor: pointer;
  font-size: 22px;
  color: var(--white);
  &:hover {
    color: var(--whiteTransparent20);
    transition: 0.1s ease-in;
  }
`;
export function EditProfilePage() {
  const { user, refreshUser } = useAuth();
  const [formData, setFormData] = useState<FormData>({
    Username: "",
    Password: "",
    image: null,
    preview: null,
  });

  const handleChange = (e: {
    target: { name: any; value: any; files: any };
  }) => {
    const { name, value, files } = e.target;
    if (name === "image") {
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

  const editImage = async () => {
    const data = new FormData();
    data.append("file", formData.image!);
    
    const res =await postAttachment("User/AddAvatar", data);
    console.log(res)
  };

  const handleSubmit = async (e: { preventDefault: () => void }) => {
    e.preventDefault();
    console.log(formData)
    if (
      formData.Password.length !== 0 ||
      (formData.Username !== user?.username && formData.Username.length !== 0)
    ) {
      const editedUser = {
        username: formData.Username,
        password: formData.Password,
      };
      
      await editUserById(editedUser);
    }
    if (formData.image !== null) {
       editImage();
    }
    setTimeout(()=>refreshUser());
  };

  const [visible, setVisible] = useState(false);
  return (
    <ProfileEditWrapper>
      <ProfileData>
        <StyledForm onSubmit={handleSubmit}>
          <StyledEditUser>
            <EditImage>
              <p>Image Preview:</p>
              <ImageDiv
                width={120}
                url={
                  formData.preview
                    ? formData.preview
                    : getUserImg(user?.avatarFileName!)
                }
              />
              <input
                type="file"
                id="image"
                name="image"
                accept=".jpg, .jpeg, .png"
                onChange={handleChange}
              />
            </EditImage>

            <TextInputsWrapper>
              <label htmlFor="Username">Username:</label>
              <InputWrapper>
                <Input
                  type="text"
                  id="Username"
                  name="Username"
                  value={formData.Username}
                  onChange={handleChange}
                  placeholder={user?.username}
                />
              </InputWrapper>
              <label htmlFor="Password">Password:</label>
              <InputWrapper>
                <Input
                  type={visible ? "text" : "password"}
                  id="Password"
                  name="Password"
                  value={formData.Password}
                  onChange={handleChange}
                />
                <EyeIcon onClick={() => setVisible(!visible)}>
                  {visible ? <VscEye /> : <VscEyeClosed />}
                </EyeIcon>
              </InputWrapper>
              <ButtonWrapper>
                <DeleteButton>Delete Account</DeleteButton>{" "}
                <Button type="submit">Submit</Button>
              </ButtonWrapper>
            </TextInputsWrapper>
          </StyledEditUser>
        </StyledForm>
      </ProfileData>
    </ProfileEditWrapper>
  );
}
