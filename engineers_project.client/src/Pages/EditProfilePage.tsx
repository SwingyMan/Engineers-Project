import { useState } from "react";
import { useAuth } from "../Router/AuthProvider";
import styled from "styled-components";
import { getImg } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";

interface FormData {
  Username: string;
  Password: string;
  image: File | null;
  preview: string | null;
}
const StyledEditUser = styled.div`
  color: var(--white);
  display: flex;
`;
const EditImage = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;
const TextInputsWrapper = styled.div``;
export function EditProfilePage() {
  const { user } = useAuth();
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

  const handleSubmit = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    const data = new FormData();
    data.append("Username", formData.Username);
    data.append("Password", formData.Password);
    if (formData.image) {
      data.append("image", formData.image);
    }

    fetch("/submit", {
      method: "POST",
      body: data,
    })
      .then((response) => response.json())
      .then((result) => {
        console.log("Success:", result);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  };
  console.log(formData.preview);
  return (
    <form onSubmit={handleSubmit}>
      <StyledEditUser>
        <EditImage>
          <p>Image Preview:</p>
          <ImageDiv
            width={120}
            url={
              formData.preview
                ? formData.preview
                : getImg(user?.avatarFileName!)
            }
          />
          <input
            type="file"
            id="image"
            name="image"
            accept=".jpg, .jpeg, .png"
            onChange={handleChange}
            required
          />
        </EditImage>
        <TextInputsWrapper>
          <label htmlFor="Username">Username:</label>
          <input
            type="text"
            id="Username"
            name="Username"
            value={formData.Username}
            onChange={handleChange}
            placeholder={user?.username}
          />
          <br />
          <br />

          <label htmlFor="Password">Password:</label>
          <input
            type="text"
            id="Password"
            name="Password"
            value={formData.Password}
            onChange={handleChange}

          />
          <br />
          <br />


          <br />
          <br />

          <button type="submit">Submit</button>
        </TextInputsWrapper>
      </StyledEditUser>
    </form>
  );
}
