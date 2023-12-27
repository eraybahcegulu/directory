import { useEffect, useState } from "react";
import ContactList from "../components/ContactList";
import { UserAddOutlined, EditOutlined, DeleteOutlined, SearchOutlined } from "@ant-design/icons";
import { Input, Modal, Button, Form, message, Result } from "antd";
import axios from "axios";
import { useDispatch } from 'react-redux';
import { fetchContactData } from '../redux-toolkit/contactSlice';
import { useNavigate } from 'react-router-dom';
import { useLocation } from 'react-router-dom';

const Home = () => {
    const [search, setSearch] = useState("");
    const [selectedRowKeys, setselectedRowKeys] = useState("");
    const [selectedRows, setSelectedRows] = useState([]);
    const [isAddContactModelOpen, setIsAddContactModelOpen] = useState(false);
    const [isEditContactModelOpen, setIsEditContactModelOpen] = useState(false);
    const [addContactForm] = Form.useForm();
    const [editContactForm] = Form.useForm();

    const [userId, setUserId] = useState('')
    const [userName, setUserName] = useState('')
    const [userEmail, setEmail] = useState('')

    const [failedAuth, setFailedAuth] = useState(false);

    const location = useLocation();

    const token = location.state?.token.toString() || localStorage.getItem('token');

    const navigate = useNavigate();

    const dispatch = useDispatch();

    useEffect(() => {
        getUserInfo();
    }, []);

    const getUserInfo = async () => {
        try {
            const userToken = {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            };

            const response = await axios.get("http://localhost:5111/api/Account/", userToken);
            const userInfo = response.data;
            setUserId(response.data.id);
            setUserName(response.data.username);
            setEmail(response.data.email);

            console.log(userInfo);
        } catch (error) {
            if (error.response && error.response.status === 401) {
                setFailedAuth(true);
                sessionStorage.clear();
                localStorage.clear();
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

    const onFinishAddContact = async (values) => {
        try {
            values.createdUserID = userId;
            console.log(values)
            await axios.post("http://localhost:5111/api/contact", values);
            message.success(
                <span>
                    <strong>{values.name} {values.surname} </strong> kişilere eklendi.
                </span>
            );
            dispatch(fetchContactData());
            addContactForm.resetFields();
            setIsAddContactModelOpen(false);
        } catch (error) {
            console.log(error);
        }
    };

    const DeleteContact = async () => {
        try {
            if (selectedRowKeys.length === 0) {
                message.info(
                    <span>
                        <strong> Lütfen silinecek kişileri seçiniz.</strong>
                    </span>
                );
                return;
            }
            setselectedRowKeys('');
            await Promise.all(selectedRowKeys.map(id => axios.delete(`http://localhost:5111/api/contact/${id}`)));
            dispatch(fetchContactData());
        } catch (error) {
            console.log(error);
        }
    };

    const isEditContact = async () => {
        if (selectedRowKeys.length !== 1) {
            message.info(
                <span>
                    Lütfen düzenlemek istediğiniz <strong> bir kişiyi </strong> seçiniz.
                </span>
            );
            return;
        }

        const firstSelectedRow = selectedRows[0];
        editContactForm.setFieldsValue({
            name: firstSelectedRow.name,
            surname: firstSelectedRow.surname,
            phone: firstSelectedRow.phone
        });
        setIsEditContactModelOpen(true);
    };

    const onFinishEditContact = async (values) => {
        values.createdUserID = userId;
        try {
            await axios.put(`http://localhost:5111/api/contact/${selectedRowKeys}`, values);
            message.success(
                <span>
                    <strong> Kişi düzenlendi. </strong>
                </span>
            );
            dispatch(fetchContactData());
            editContactForm.resetFields();
            setselectedRowKeys('');
            setSelectedRows([]);
            setIsEditContactModelOpen(false);
        } catch (error) {
            console.log(error);
        }
    };

    const logout = () => {
        localStorage.clear();
        sessionStorage.clear();
        navigate('/')
    }


    if (sessionStorage.getItem("token")) {
        window.addEventListener('beforeunload', function () {
            sessionStorage.clear();
        });

        window.addEventListener('popstate', function () {
            sessionStorage.clear();
        });
    }

    const handle404 = () => {
        navigate('/');
    };

    return (
        <>

            {
                (failedAuth === true || !(sessionStorage.getItem("token") || localStorage.getItem("token")))
                    ?

                    <>
                        <Result
                            status="404"
                            title="404"
                            subTitle="Başarısız yönlendirme, giriş yapmalısın."
                            extra={<Button onClick={handle404} type="primary">Anasayfaya Dön</Button>}
                        />
                    </>

                    :

                    <div className="bg-slate-400 min-h-screen flex flex-col items-center gap-5">

                        <div className="mt-20 flex flex-col justify-center items-center gap-2 border border-black border-1 rounded-xl p-2">

                            <span>Hoşgeldin {userName}</span>
                            <span>{userEmail}</span>
                            <Button onClick={logout} type="primary" danger>
                                Çıkış
                            </Button>


                        </div>

                        <div className="flex flex-row item-center justify-center mt-10 gap-4">

                            <Input onChange={(e) => setSearch(e.target.value.toLowerCase())} size="large" prefix={<SearchOutlined />} />

                            <UserAddOutlined onClick={() => setIsAddContactModelOpen(true)} className="hover:cursor-pointer text-green-700 hover:text-green-600 transition-all text-2xl" />

                            <EditOutlined onClick={isEditContact} className="hover:cursor-pointer text-blue-600 hover:text-blue-500 transition-all text-2xl" />

                            <DeleteOutlined onClick={DeleteContact} className="hover:cursor-pointer text-red-600 hover:text-red-500 transition-all text-2xl" />

                        </div>

                        <div>
                            <ContactList search={search} selectedRowKeys={selectedRowKeys} setSelectedRowKeys={setselectedRowKeys} selectedRows={selectedRows} setSelectedRows={setSelectedRows} />
                        </div>

                        <Modal
                            open={isAddContactModelOpen}
                            onCancel={() => setIsAddContactModelOpen(false)}
                            footer={false}
                        >
                            <h2>
                                <strong>KİŞİ EKLE</strong>
                            </h2>
                            <Form
                                className="mt-4 flex flex-col gap-4"
                                layout="vertical"
                                onFinish={onFinishAddContact}
                                form={addContactForm}
                            >
                                <Form.Item
                                    name="name"
                                    label="Ad"
                                    rules={[
                                        { required: true, message: "Ad boş bırakılamaz" },
                                        { max: 20, message: "Ad en fazla 20 karakter olabilir" }
                                    ]}
                                >
                                    <Input style={{ borderRadius: "0" }} size="large" />
                                </Form.Item>

                                <Form.Item
                                    name="surname"
                                    label="Soyad"
                                    rules={[{ required: true, message: "Soyad boş bırakılamaz" },
                                    { max: 20, message: "Soyad en fazla 20 karakter olabilir" }
                                    ]}
                                >
                                    <Input style={{ borderRadius: "0" }} size="large" />
                                </Form.Item>

                                <Form.Item
                                    name="phone"
                                    label="Telefon No"
                                    rules={[{ required: true, message: "Telefon No boş bırakılamaz" },
                                    { max: 20, message: "Telefon No en fazla 20 karakter olabilir" }
                                    ]}
                                >
                                    <Input style={{ borderRadius: "0" }} size="large" />
                                </Form.Item>

                                <Form.Item className="flex justify-end mb-0">
                                    <Button
                                        style={{ borderRadius: "0" }}
                                        type="primary"
                                        htmlType="submit"
                                        size="large"
                                    >
                                        <strong> EKLE </strong>
                                    </Button>
                                </Form.Item>
                            </Form>
                        </Modal>

                        <Modal
                            open={isEditContactModelOpen}
                            onCancel={() => setIsEditContactModelOpen(false)}
                            footer={false}
                        >
                            <h2>
                                <strong>KİŞİ DÜZENLE</strong>
                            </h2>
                            <Form
                                className="mt-4 flex flex-col gap-4"
                                layout="vertical"
                                onFinish={onFinishEditContact}
                                form={editContactForm}
                            >
                                <Form.Item
                                    name="name"
                                    label="Ad"
                                    rules={[{ required: true, message: "Ad boş bırakılamaz" },
                                    { max: 20, message: "Ad en fazla 20 karakter olabilir" }
                                    ]}
                                >
                                    <Input style={{ borderRadius: "0" }} size="large" />
                                </Form.Item>

                                <Form.Item
                                    name="surname"
                                    label="Soyad"
                                    rules={[{ required: true, message: "Soyad boş bırakılamaz" },
                                    { max: 20, message: "Soyad en fazla 20 karakter olabilir" }
                                    ]}
                                >
                                    <Input style={{ borderRadius: "0" }} size="large" />
                                </Form.Item>

                                <Form.Item
                                    name="phone"
                                    label="Telefon No"
                                    rules={[{ required: true, message: "Telefon No boş bırakılamaz" },
                                    { max: 20, message: "Telefon No en fazla 20 karakter olabilir" }
                                    ]}
                                >
                                    <Input style={{ borderRadius: "0" }} size="large" />
                                </Form.Item>

                                <Form.Item className="flex justify-end mb-0">
                                    <Button
                                        style={{ borderRadius: "0" }}
                                        type="primary"
                                        htmlType="submit"
                                        size="large"
                                    >
                                        <strong> GÜNCELLE </strong>
                                    </Button>
                                </Form.Item>
                            </Form>
                        </Modal>

                    </div>
            }
        </>


    );
};

export default Home;