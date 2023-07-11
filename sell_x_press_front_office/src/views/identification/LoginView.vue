<template>
    <div class="container">
        <div class="identification-container col-11 col-md-5">
            <h1>Sign Up</h1>
            <login-form @input-form="updateData" />
            <button class="btn btn-secondary signup-button">Sign Up</button>
            <a class="link-dark mb-4" href="/login">You already have an account ?</a>
        </div>
    </div>
</template>

<script>
    import LoginForm from '@/components/identification/LoginForm.vue';
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
    margin-top: 5vh;
    margin-bottom: 5vh;
}

a{
    align-self: flex-end;
    margin-right: 2vw;
}
</style>