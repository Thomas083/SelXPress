<template>
    <div class="user-container">
        <div class="user-container-content">
            <h3 class="user-title">Personal Information</h3>
            <user-profil-form @input-form="updateData" />
            <div class="user-btns-container">
                <button class="btn btn-primary change-password-btn">Change Password</button>
                <button class="btn btn-secondary save-information-btn" v-on:click="updateUser">Save</button>
            </div>
        </div>
    </div>
</template>

<script>
import { PUT } from "@/api/axios";
import { ENDPOINTS } from "@/api/endpoints";
import UserProfilForm from "@/components/UserProfil/UserProfilForm.vue";
import { createToast } from "mosha-vue-toastify";

export default {
    name: 'UserView',
    components: {
        UserProfilForm
    },
    data() {
        return {
            formData: null
        }
    },
    methods: {
        updateData(e) {
            this.formData = e;
        },
        updateUser() {
            PUT(ENDPOINTS.UPDATE_USER, this.formData, JSON.parse(localStorage.getItem("user")).token)
            .then((response) => {
                console.dir(response)
                createToast({ title: 'Successful Update', description: 'Your profil was sucessfuly updated' }, { type: 'success', position: 'bottom-right' });
            })
            .catch((error) => {
                console.dir(error)
                createToast(`An error occured... Please try again`, { type: 'danger', position: 'bottom-right' });
            })
        },
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
</style>