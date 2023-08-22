<template>
    <div class="container">
        <div class="identification-container col-11 col-md-5">
            <h1>Sign In</h1>
            <login-form @input-form="updateData" />
            <button class="btn btn-primary signin-button" @click="logUser()">Sign In</button>
            <a class="link-dark" href="/forgot">Forgot password ?</a>
            <button class="btn btn-secondary signup-button" @click="goToSignUp()">Sign Up</button>
        </div>
    </div>
</template>

<script>
import LoginForm from '@/components/identification/LoginForm.vue';
import { POST } from '@/api/axios';
import { ENDPOINTS } from '@/api/endpoints'
import { createToast } from 'mosha-vue-toastify';

export default {
    name: 'LoginView',
    components: {
        LoginForm,
    },
    data() {
        return {
            formData: null,
        }
    },
    methods: {
        updateData(e) {
            this.formData = e;
        },
        goToSignUp() {
            this.$router.push({ path: '/register' });
        },
        logUser() {
            POST(ENDPOINTS.USER_LOGIN, this.formData)
                .then((userCredential) => {
                    createToast({ title: 'Sign IN Success', description: 'You are sucessfuly connected' }, { type: 'success', position: 'bottom-right' });
                    const user = {
                        email: userCredential.data.email,
                        token: userCredential.data.token
                    }
                    localStorage.setItem("user", JSON.stringify(user));
                    this.$router.push({ path: '/' });
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
                });
        },
    }
}
</script>

<style scoped>
.container {
    display: flex;
    justify-content: center;
    align-items: center;
    max-height: 80vh;
    width: 100%;
    padding-top: 5vh;
    padding-bottom: 5vh;
}

.identification-container {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    background-color: #FFFFFF;
    border-radius: 10px;
    padding-top: 3vh;
}

.signup-button{
    align-self: flex-end;
    margin-right: 2vw;
    margin-bottom: 2vh;
}

a {
    align-self: flex-end;
    margin-right: 2vw;
}

.signin-button{
    margin-top: 5vh;
    margin-bottom: 5vh;
}
</style>