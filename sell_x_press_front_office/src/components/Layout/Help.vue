<template>
    <div class="modal-layout">
        <div class="modal-container">
            <div class="modal-title">Help Us To Upgrade</div>
            <form class="modal-form">
                <textarea class="modal-form-input" type="text" placeholder="Write your comment here..." ></textarea>
                <div class="modal-form-btn-container">
                    <button class="modal-form-btn">
                        SEND
                        <img width="20vw" src='../../assets/Modal/send.png' />
                    </button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>

export default {
    name: "Help",
};

</script>

<style scoped>
.modal-layout {
    width: 100vw;
    height: 100vh;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    position: fixed;
    background: rgba(94, 94, 94, 0.74);
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

}

.modal-container {
    padding: 3rem 5rem;
    border-radius: 50px;
    background-color: var(--main-white);
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.modal-title {
    font-size: 3rem;
    font-family: url(../../font/Inter/static/Inter-SemiBold.ttf);
    margin-bottom: 5vh;
}

.modal-form {
    display: flex;
    flex-direction: column;
    gap: 2rem;
    border-radius: 2rem;
}

.modal-form-input {
    border-radius: 1rem;
    padding: 1rem;
    height: 30vh;
    resize: none;
}

.modal-form-btn-container {
    display: flex;
    justify-content: flex-end;
}

.modal-form-btn {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    background-color: var(--main-orange);
    color: var(--main-white);
    font-family: url(../../font/Inter/static/Inter-SemiBold.ttf);
    font-size: 1rem;
    padding: 1rem 3rem;
    gap: 1rem;
    border-radius: 1rem;
    border: none
}

.modal-form-btn:hover {
    opacity: 0.5;
}
</style>