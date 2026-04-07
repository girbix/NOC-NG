// site.js - Funzioni client per NOC-NG (tema, modali, utility)

// 1. TEMA (dark/light mode)
function applyTheme() {
    const savedTheme = localStorage.getItem('theme') || 'dark-mode';
    document.body.className = savedTheme;
}

function toggleTheme() {
    const isDark = document.body.classList.contains('dark-mode');
    const newTheme = isDark ? 'light-mode' : 'dark-mode';
    document.body.className = newTheme;
    localStorage.setItem('theme', newTheme);
}

function initThemeToggle() {
    const toggleBtn = document.getElementById('themeToggle');
    if (toggleBtn) toggleBtn.addEventListener('click', toggleTheme);
}

// 2. MODAL (per creare nuovo incidente – usato nella dashboard client)
function openModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) modal.style.display = 'block';
}

function closeModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) modal.style.display = 'none';
}

// Chiudi modale cliccando fuori dal contenuto
window.onclick = function(event) {
    const modals = document.querySelectorAll('.modal');
    modals.forEach(modal => {
        if (event.target === modal) {
            modal.style.display = 'none';
        }
    });
}

// 3. POPOLAMENTO DINAMICO DI DATALIST (per service offering, ci, location, ecc.)
// Queste funzioni possono essere chiamate dalle pagine che ne hanno bisogno
function populateDatalist(listId, items) {
    const datalist = document.getElementById(listId);
    if (!datalist) return;
    datalist.innerHTML = '';
    items.forEach(item => {
        const option = document.createElement('option');
        option.value = item;
        datalist.appendChild(option);
    });
}

// 4. GESTIONE LOGOUT (se si usa link con classe logout-link)
function setupLogout() {
    const logoutLink = document.getElementById('logoutLink');
    if (logoutLink) {
        logoutLink.addEventListener('click', (e) => {
            e.preventDefault();
            // Chiamata al controller di logout
            window.location.href = '/Account/Logout';
        });
    }
}

// 5. INIZIALIZZAZIONE GENERICA ALL'AVVIO
document.addEventListener('DOMContentLoaded', () => {
    applyTheme();
    initThemeToggle();
    setupLogout();

    // Se siamo nella dashboard client, inizializza il bottone per il modal
    const newIncidentBtn = document.getElementById('newIncidentBtn');
    if (newIncidentBtn) {
        newIncidentBtn.addEventListener('click', () => openModal('incidentModal'));
    }

    // In tutte le pagine con un modal, collega il pulsante di chiusura (X)
    document.querySelectorAll('.close').forEach(closeBtn => {
        closeBtn.addEventListener('click', (e) => {
            const modal = e.target.closest('.modal');
            if (modal) modal.style.display = 'none';
        });
    });
});
