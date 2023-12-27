import { useState } from "react";
import { Button, Form, Input, Spin, message, Switch } from 'antd';
import axios from "axios";
import { useNavigate } from 'react-router-dom';

const LoginForm = () => {
    const [isLogin, setIsLogin] = useState(false);
    const [isChecked, setChecked] = useState(false);

    const navigate = useNavigate();

    const onFinish = async (values) => {
        try {
            setIsLogin(true);
            const res = await axios.post("http://localhost:5111/api/Account/Login", values);
            const token = res.data.token;

            if (isChecked === true) {

                localStorage.setItem("token", token);

            }
            else {

                sessionStorage.setItem("token", token);
            }

            navigate(`/home`, { state: { token } });
            setIsLogin(false);

        } catch (error) {
            setIsLogin(false);
            if (error.response && error.response.status === 400) {
                message.error(
                    <span>
                        <strong>Girilen Bilgiler Yanlış.</strong>
                    </span>
                );
            } else {
                message.error(
                    <span>
                        <strong> Server Error. </strong>
                    </span>
                );
            }

            console.error(error);
        }
    };

    const onChange = () => {
        setChecked(prevState => {
            return !prevState;
        });
    };

    return (

        <Form
            name="basic"
            labelCol={{
                span: 8,
            }}
            wrapperCol={{
                span: 16,
            }}
            style={{
                maxWidth: 600,
            }}
            initialValues={{
                remember: true,
            }}
            onFinish={onFinish}
            autoComplete="off"
        >

            <Form.Item
                label={<span style={{ color: 'white' }}>Ad/E-Posta:</span>}
                name="usernameOrEmail"
                rules={[
                    {
                        required: true,
                        message: 'Lütfen kullanıcı adı/email girin!',
                    },
                ]}
            >
                <Input />
            </Form.Item>

            <Form.Item
                label={<span style={{ color: 'white' }}>Şifre:</span>}
                name="password"
                rules={[
                    {
                        required: true,
                        message: 'Lütfen şifrenizi girin!',
                    },
                ]}
            >
                <Input.Password />
            </Form.Item>

            <Form.Item
                wrapperCol={{
                    offset: 8,
                    span: 16,
                }}
            >
                <div className="flex flex-row justify-start items-center gap-2">
                    <Button type="primary" htmlType="submit">
                        Giriş Yap
                    </Button>

                    <div className="flex flex-col justify-center items-center">
                        <Switch defaultChecked={isChecked} onChange={onChange} />
                        <span className="text-white text-xs">Beni Hatırla</span>
                    </div>

                    {


                        isLogin
                        &&
                        <div className="flex flex-row justify-center items-center">
                            <Spin size="large" />
                        </div>

                    }


                </div>
            </Form.Item>
        </Form>
    )
}

export default LoginForm