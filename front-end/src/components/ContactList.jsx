import { useEffect } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { fetchContactData } from '../redux-toolkit/contactSlice';
import { Spin, Table, Alert } from "antd";


const ContactList = ({ search, selectedRowKeys, setSelectedRowKeys, setSelectedRows }) => {

    const rowSelection = {
        selectedRowKeys,
        onChange: (selectedRowKeys, selectedRows) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`);
            selectedRows.forEach(row => {
                console.log('Selected Row Name:', row.name);
            });

            setSelectedRowKeys(selectedRowKeys);
            setSelectedRows(selectedRows);
        },
    };


    const dispatch = useDispatch();
    const contact = useSelector(state => state.contact.data);
    const status = useSelector(state => state.contact.status);

    useEffect(() => {
        dispatch(fetchContactData());
    }, [dispatch]);

    const columns = [
        {
            title: 'AD',
            dataIndex: 'name',
        },
        {
            title: 'SOYAD',
            dataIndex: 'surname',
        },
        {
            title: 'TELEFON NO',
            dataIndex: 'phone',
        },
    ];

    const filteredContacts = contact.filter(
        (item) => item.phone.includes(search.trim())
        || (item.name + ' ' + item.surname).toLowerCase().includes(search.trim())
    );

    return (
        <>

            {status === 'loading' && <div className="text-center"> <Spin size="large" /> </div>}

            {status === 'succeeded' && filteredContacts.length > 0 && (
                <Table
                    scroll={{ y: 400 }}
                    className="md:max-w-[600px] max-w-[450px]"

                    rowSelection={{
                        type: 'checkbox',
                        ...rowSelection,
                    }}
                    rowKey="id"
                    columns={columns}
                    dataSource={filteredContacts}
                />
            )}

            {status === 'succeeded' && filteredContacts.length === 0 && (
                <Alert message="Kişi Bulunamadı" type="warning" />
            )}

            {status === 'failed' &&
                <Alert message="Kişiler aranırken hata oluştu." type="error" />}
        </>
    );
};

export default ContactList;