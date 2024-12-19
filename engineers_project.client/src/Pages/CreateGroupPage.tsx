import {  useState } from "react";
import styled from "styled-components";
import { postAttachment } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";
import defaultGroup from "../assets/defaultGroup.jpg"
import { useNavigate } from "react-router";
import { createGroup } from "../API/services/groups.service";


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
const NewGroupWrapper = styled.div`
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
const CancelButton = styled.button`
  padding: 10px 15px;
  font-size: 1rem;
  border: none;
  width: 20%;
  border-radius: 4px;
  cursor: pointer;
  background-color: gray;
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

export function CreateGroupPage() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<FormData>({
    groupID: "",
    groupName: "",
    groupDescription:"",
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

  const editImage = (id:string) => {
    const data = new FormData();
    data.append("file", formData.image!);
    data.append("GroupId", id )
    postAttachment("Group/AddImage", data);
  };

  const handleSubmit = async (e: { preventDefault: () => void }) => {
    e.preventDefault();

    if (
      formData.groupDescription !== "" &&
      formData.groupName !== ""
    ) {
      const newGroup = {
        name:formData.groupName,
        description: formData.groupDescription,
      };
      try{
        const res = await createGroup(newGroup) 

        if (res){

          setFormData({...formData,groupID:res.id})
          if (formData.image !== null ) {
            editImage(res.id);
          }
        }
        
        setTimeout(()=>navigate(`/group/${res.id}`))
      }
      catch(e){
        console.log(e)
      }
    }
    
  };

  return (
    <NewGroupWrapper>
      <GroupData>
      <h3>Crate New Group</h3>
        <StyledForm onSubmit={handleSubmit}>
          <StyledEditGroup>
            <EditImage>
              <p>Image Preview:</p>
              <ImageDiv
                width={120}
                url={
                  formData.preview
                    ? formData.preview
                    : defaultGroup
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
                placeholder={"Group name"}
              />
              <br />

              <label htmlFor="groupDescription">Group description:</label>

              <TextArea
                id="groupDescription"
                name="groupDescription"
                value={formData.groupDescription}
                placeholder={"Group description"}
                onChange={handleChange}
              />
              <br />
              <ButtonWrapper>
                <CancelButton onClick={()=>navigate(-1)}>Cancel</CancelButton>
                <Button type="submit">Create group</Button>
              </ButtonWrapper>
            </TextInputsWrapper>
          </StyledEditGroup>
        </StyledForm>
      </GroupData>
    </NewGroupWrapper>
  );
}
