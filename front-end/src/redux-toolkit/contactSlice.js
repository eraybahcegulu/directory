import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

const GET_CONTACT_API_URL = "http://localhost:5111/api/contact";

export const fetchContactData = createAsyncThunk('fetchContactData', async () => {
    try {
        const response = await axios.get(GET_CONTACT_API_URL);
        return response.data;
    } catch (error) {
        console.error(error);
        throw error;
    }
});

const contactSlice = createSlice({
    name: 'contact',
    initialState: {
        data: [],
        status: 'idle',
    },

    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchContactData.pending, (state) => {
                state.status = 'loading';
            }) // createAsyncThunk => fetchContactData asenkron işlemi devam ediyor.

            .addCase(fetchContactData.fulfilled, (state, action) => {
                state.status = 'succeeded';
                state.data = action.payload;
            }) //createAsyncThunk => fetchContactData asenkron işlemi tamamlandı. (fulfilled)
            
            .addCase(fetchContactData.rejected, (state, action) => {
                state.status = 'failed';
                state.error = action.error.message;
            });//createAsyncThunk => fetchContactData asenkron işlemi hata ile sonuçlandı.
    },
});

export default contactSlice.reducer;