<template>
    <div class="container">
        <div class="identification-container col-11 col-md-5">
            <h1>Sign In</h1>
            <register-form @input-form="updateData" />
            <button class="btn btn-primary signin-button">Sign In</button>
            <a class="link-dark" href="/forgot">Forgot password ?</a>
            <button class="btn btn-secondary signup-button" @click="goToSignUp">Sign Up</button>
        </div>
    </div>
</template>

<script>
import RegisterForm from '@/components/identification/RegisterForm.vue';
    export default {
        name: 'RegisterView',
        components: {
            RegisterForm
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
                this.$router.push('/register');
            }
        },      
    }
</script>

<style scoped>
.container{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 80vh;
    width: 100%;
    padding-top: 5vh;
    padding-bottom: 5vh;
}

.identification-container{
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    height: 70vh;
    background-color: #FFFFFF;
    border-radius: 10px;
    padding-top: 3vh;
}

.signup-button{
    align-self: flex-end;
    margin-right: 2vw;
    margin-bottom: 2vh;
}

a{
    align-self: flex-end;
    margin-right: 2vw;
}

.signin-button{
    margin-top: 5vh;
    margin-bottom: 5vh;
}
</style>