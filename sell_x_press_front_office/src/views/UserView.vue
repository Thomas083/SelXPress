<template>
    <div class="user-container">
        <div class="user-container-content">
            <h3 class="user-title">Personal Information</h3>
            <user-profile-form @input-form="updateData" />
            <div class="user-btns-container">
                <button class="btn btn-primary change-password-btn" v-on:click="resetPassword">Change Password</button>
                <button class="btn btn-primary business-status-btn" v-on:click="setBusinesStatusState">Apply for Business Status</button>
                <business-status-form v-if="businessStatusState"/>
                <button class="btn btn-secondary save-information-btn" v-on:click="updateUser">Save</button>
            </div>
        </div>
        <div class="user-history-container">
            <h3 class="history-title">Order History</h3>
            <button class="btn btn-primary history-btn" @click="goToHistory()">See my Order</button>
        </div>
    </div>
</template>

<script>
import UserProfileForm from "@/components/UserProfile/UserProfileForm.vue";
import BusinessStatusForm from "@/components/UserProfile/BusinessStatusForm.vue";
import { auth } from "@/config/firebase-config";
import { sendPasswordResetEmail } from "firebase/auth";
import { createToast } from 'mosha-vue-toastify';
import { PUT } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";

export default {
    name: 'UserView',
    components: {
        UserProfileForm,
        BusinessStatusForm
    },
    data() {
        return {
            formData: null,
            businessStatusState: false,
        }
    },
    methods: {
        updateData(e) {
            this.formData = e;
        },
        setBusinesStatusState() {
            this.businessStatusState = !this.businessStatusState
        },
        resetPassword() {
            sendPasswordResetEmail(auth, "maxence.leroy59000@gmail.com")
            .then(() => {
                createToast("An Email was sent to reset your password", {type: 'success', position: 'bottom-right'});
            });
        },
        goToHistory() {
            this.$router.push({ path: '/history' });
        },
        updateUser() {
            PUT(ENDPOINTS.UPDATE_USER, this.formData, JSON.parse(localStorage.getItem("user")).token)
            .then((response) => {
                createToast({ title: 'Successful Update', description: 'Your profil was sucessfuly updated' }, { type: 'success', position: 'bottom-right' });
            })
            .catch((error) => {
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            })
        },
    },
}

</script>

<style scoped>
.user-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
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

.user-title,
.history-title {
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

}

.save-information-btn {
    border-radius: 9px;
    border: none;
    width: 10vw;

}

.business-status-btn {
    border-radius: 9px;
    padding: 0.5rem 4rem;
    border: none;

}

.history-btn {
    border: 9px;
    padding: 0.5rem 4rem;
    border: none;
    box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.user-history-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    justify-content: center;
    align-items: center;
    background-color: var(--main-white);
    height: 20vh;
    width: 20vw;
    border-radius: 10px;
    margin-top: 1rem;
    margin-left: 1rem;
}
</style>