import React from "react"
import {useAuthStore} from "../../../store/useAuthStore"
import {useNavigate} from "react-router-dom"
import Button from "../../../ui/Button"
import {ROUTES} from "../../../routes/routes"


export default function HomePage(){
    const { userData, setUserData} = useAuthStore()
    const navigate = useNavigate()
    return (
        <>
            {
                userData?.isAdmin
                ?
                    <>
                        <h1>Hi admin</h1>
                        <Button onClick={() => navigate(ROUTES.ADMIN)} text={"To admin page"}/>
                    </>
                :
                    <h1>Home, sweet home :)</h1>
            }
            <Button text={"LOG OUT"} isOutlined={true} onClick={()=>setUserData(null)}/>
        </>
    )

}
