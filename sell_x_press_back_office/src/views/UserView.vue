<template>
    <div class="user-container">
        <div class="user-container-content">
            <h3 class="user-title">Personal Information</h3>
            <user-profil-form />
            <div class="user-btns-container">
                <button class="btn btn-primary change-password-btn">Change Password</button>
                <button class="btn btn-secondary save-information-btn">Save</button>
            </div>
        </div>
    </div>
</template>

<script>
import UserProfilForm from "@/components/UserProfil/UserProfilForm.vue";

export default {
    name: 'UserView',
    components: {
        UserProfilForm
    }
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