import "../styles/Login.css";
import { ArrowRightOutlined, LogoutOutlined } from "@ant-design/icons";
import LoginForm from '../components/LoginForm'
import { useNavigate } from 'react-router-dom';

const Login = () => {

    const token = localStorage.getItem("token");

    const navigate = useNavigate();
    const logout = () => {
        localStorage.clear();
        sessionStorage.clear();
        navigate('/')
    }

    return (
        <div className="h-screen w-screen bg-gradient-to-b from-color to-color md:flex md:flex-row md:justify-around p-4 flex flex-col justify-around">

            <div className="flex flex-col items-center justify-around text-7xl md:text-8xl">
                <div className="text-center text-white">
                    <p>DIRECTORY</p>
                </div>

            </div>

            <div className="flex justify-center items-end">

            {
                    token
                        ?
                        <div className="flex flex-row gap-5">
                            <p className="text-white text-2xl">Giriş Bulundu</p>
                            <ArrowRightOutlined onClick={() => navigate("/home", {state: {token}})} className="text-2xl hover:scale-125 cursor-pointer text-blue-500 hover:text-blue-400 transition-all" />
                            <LogoutOutlined onClick={logout} className="text-2xl hover:scale-125 cursor-pointer text-red-500 hover:text-red-400 transition-all" />
                        </div>
                        :
                        <LoginForm />
                }


            </div>
        </div>
    )
}

export default Login