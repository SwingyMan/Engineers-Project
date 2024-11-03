import styled from "styled-components";

const StyledChatBox = styled.div`
    
`
export function ChatBox(){
    return(
        <StyledChatBox>
            {/* header */}
            <div>
                {/* image */}
                {/* chatname */}
                {/* options */}
            </div>
            {/* chat */}
            <div>

            </div>
            {/* sendbar */}
            <div>
                <form>
                <div>
                    <input type="text"/>
                </div>
                <div>
                    <input type="submit"/>
                </div>
                </form>
            </div>
        </StyledChatBox>
    );
}