<template>
    <div class="user-container">
        <div class="user-container-content">
            <h3 class="user-title">Personal Information</h3>
            <user-profile-form />
            <div class="user-btns-container">
                <button class="btn btn-primary change-password-btn" v-on:click="resetPassword">Change Password</button>
                <button class="btn btn-primary business-status-btn" v-on:click="setBusinesStatusState">Apply for Business Status</button>
                <business-status-form v-if="businessStatusState"/>
                <button class="btn btn-secondary save-information-btn">Save</button>
            </div>
        </div>
    </div>
</template>

<script>
import UserProfileForm from "@/components/UserProfile/UserProfileForm.vue";
import BusinessStatusForm from "@/components/UserProfile/BusinessStatusForm.vue";
import { auth } from "@/config/firebase-config";
import { sendPasswordResetEmail } from "firebase/auth";
import { createToast } from 'mosha-vue-toastify';

export default {
    name: 'UserView',
    components: {
        UserProfileForm,
        BusinessStatusForm
    },
    data() {
        return {
            businessStatusState: false,
        }
    },
    methods: {
        setBusinesStatusState() {
            this.businessStatusState = !this.businessStatusState
        },
        resetPassword() {
            sendPasswordResetEmail(auth, "maxence.leroy59000@gmail.com")
            .then(() => {
                createToast("An Email was sent to reset your password", {type: 'success', position: 'bottom-right'});
            });
        }
    },
}

</script>

<style scoped>
.user-container {
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 2rem;
    margin-bottom: 2rem;
}

.user-container-content {
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    gap: 1rem;
    background-color: var(--main-white);
    border-radius: 10px;
    margin-top: 1rem;
    margin-bottom: 1rem;
    width: 50vw;
}

.user-title {
    text-align: center;
    font-weight: bold;
}

.user-btns-container {
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    gap: 2rem;
    width: 100%;
    margin-top: 5vh;
    margin-right: 5vw;
    margin-bottom: 5vh;
}

.change-password-btn {
    border-radius: 9px;
    padding: 0.5rem 2rem;
    border: none;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.save-information-btn {
    border-radius: 9px;
    border: none;
    width: 10vw;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.business-status-btn {
    border-radius: 9px;
    padding: 0.5rem 4rem;
    border: none;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}
</style>