<template>
    <div :onChange="$emit('inputForm', formData)" class="form-container">
        <label for="input-uname">Username</label>
        <input id="input-uname" name="input-uname" type="text" v-model="formData.username" />
        <label for="input-mail">Email Address</label>
        <input id="input-mail" name="input-mail" type="email" v-model="formData.email" />
    </div>
</template>
  
<script>
import { GET } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints';


export default {
    name: 'UserProfileForm',
    data() {
        return {
            formData: {
                username: '',
                email: '',
            },
        }
    },
    mounted () {
        GET(ENDPOINTS.GET_ONE_USER, JSON.parse(localStorage.getItem("user")).token)
        .then((user) => {
            this.formData.username = user.data.username;
            this.formData.email = user.data.email;
        })
        .catch((error) => {
            console.dir(error)
        });
    },
}
</script>
  
<style scoped>
.form-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0.5rem;
    height: 75%;
    width: 100%;
    border-radius: 10px;
    padding: 0 3rem;
}

label {
    align-self: start;
    margin-left: 3vw;
    font-size: 15px;
    font-weight: bold;
}

input {
    border-radius: 15px;
    height: 53px;
    width: 90%;
    text-align: center;
    font-size: 20px;
    font-weight: bold;
}
</style>