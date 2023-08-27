<template>
    <div class="container">
        <div class="identification-container col-11 col-md-5">
            <h1>Sign Up</h1>
            <register-form @input-form="updateData" />
            <button class="btn btn-secondary signup-button" @click="createUser()">Sign Up</button>
            <a class="link-dark mb-4" href="/login">You already have an account ?</a>
        </div>
    </div>
</template>

<script>
import RegisterForm from '@/components/identification/RegisterForm.vue';
import { createToast } from 'mosha-vue-toastify';
import {POST} from "@/api/axios";
import {ENDPOINTS} from "@/api/endpoints";

export default {
    name: 'RegisterView',
    components: {
        RegisterForm,
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
        createUser() {
            POST(ENDPOINTS.CREATE_USER, this.formData)
                .then(() => {
                    createToast({ title: 'Sign UP Success', description: 'You are sucessfully registered !' }, { type: 'success', position: 'bottom-right' });
                    POST(ENDPOINTS.USER_LOGIN, {email: this.formData.email, password: this.formData.password})
                        .then((userCredential) => {
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
                })
                .catch(() => {
                    createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
          });
        }
    },
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

.signup-button {
    margin-top: 5vh;
    margin-bottom: 5vh;
}

a {
    align-self: flex-end;
    margin-right: 2vw;
}
</style>